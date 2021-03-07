using System;
using System.Collections.Generic;

namespace Modifiers
{
    // Еще она наследует MonoBehaviour ну или что там нужно.
    public abstract class Entity : IDataComponentProvider
    {
        public Dictionary<Type, DataComponent> DataComponents { get; private set; }

        protected Entity()
        {
            DataComponents = new Dictionary<Type, DataComponent>();
        }
        
        // МЕТОДЫ ПРОВАЙДЕРА. 

        // Этот метод может быть вызван из модификатора@
        // Поэтому я решил добавить результат. Например если один модификатор не добавился,
        // то и его родитель тоже не добавиться.
        public ModifierAddResult AddModifier(DataComponentModifier modifier)
        {
            // @Вот здесь может быть вызван AddModifier@
            var result = modifier.RequestDataComponents(this);

            switch (result)
            {
                // Реализации я писать не стал, ибо зависит от многих факторов.
                case DataComponentRequestResult.Accept: return ModifierAddResult.Added;
                case DataComponentRequestResult.Discard: return ModifierAddResult.Discarded;
                case DataComponentRequestResult.WaitForData: return ModifierAddResult.Added;
            }

            // По идее сюда мы дойти не должны. Но так как в c# нет паттерн матчинга хорошего,
            // то мы можем попросту забыть реализовать новый RequestResult.
            throw new Exception();
        }
        
        // Может возникнуть вопрос почему мы возвращаем при запросе Option<TDataComponent>.
        // Данный тип сигнализирует о том, что результат может вернуться в двух вариантах. 
        // Варианты: Some and None. Да, мы можем вернуть просто null, но как мы тогда узнаем,
        // что метод его будет возвращать. А вдруг не будет, или все же будет...
        
        // Почему null отстой, а Option для тру ребят -------->
        // https://www.westerndevs.com/Fsharp/Functional-programming/maybe-null-is-not-an-option/
        
        // Tony Hoare calls null references his billion dollar mistake. Using null values (NULL, Null, nil, etc)
        // makes code harder to maintain and to understand.
        
        // Данная реализация Option является структурой.

        // Да, здесь копипастинг кода, но это делается единожды. Кроме того провайдером необязательно должна
        // являться сущность. Просто в данном случае так быстрее было написать. Главное чтобы провайдер
        // имел доступ к компонентам сущности.
        public Option<DataComponent> RequestComponent<T>() where T : DataComponent
        {
            DataComponent data;
            if (!DataComponents.TryGetValue(typeof(T), out data))
                return Option<DataComponent>.None;

            return data;
        }

        public Option<ValueTuple<DataComponent, DataComponent>> RequestComponent<T, TU>() 
            where T : DataComponent where TU : DataComponent
        {
            DataComponent data;
            if (!DataComponents.TryGetValue(typeof(T), out data))
                return Option<ValueTuple<DataComponent, DataComponent>>.None;
            
            DataComponent data1;
            if (!DataComponents.TryGetValue(typeof(TU), out data1))
                return Option<ValueTuple<DataComponent, DataComponent>>.None;

            return new ValueTuple<DataComponent, DataComponent>(data, data1);
        }

        public Option<ValueTuple<DataComponent, DataComponent, DataComponent>> RequestComponent<T, TU, TA>()
            where T : DataComponent where TU : DataComponent where TA : DataComponent
        {
            DataComponent data;
            if (!DataComponents.TryGetValue(typeof(T), out data))
                return Option<ValueTuple<DataComponent,DataComponent, DataComponent>>.None;
            
            DataComponent data1;
            if (!DataComponents.TryGetValue(typeof(TU), out data1))
                return Option<ValueTuple<DataComponent,DataComponent, DataComponent>>.None;
            
            DataComponent data2;
            if (!DataComponents.TryGetValue(typeof(TA), out data2))
                return Option<ValueTuple<DataComponent,DataComponent, DataComponent>>.None;
            
            return new ValueTuple<DataComponent,DataComponent, DataComponent>(data, data1, data2);
        }
    }
}