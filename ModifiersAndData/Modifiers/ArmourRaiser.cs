namespace Modifiers
{
    public class ArmourRaiser : DataComponentModifier
    {
        // Кешированные значения. Для них кстати можно было бы использовать WeakReference. 
        // С weakreference можно было бы частично избавиться от Dispose.
        private Health _health;
        private Armour _armour;
        
        public override DataComponentRequestResult RequestDataComponents(IDataComponentProvider provider)
        {
            // Получаем пару значений типа структуры ValueTuple
            var data = provider.RequestComponent<Armour, Health>();
            
            // Если все нужные компоненты присутствуют.
            if(data.IsSome)
            {
                // Кешируем
                _armour = data.Value.Item1 as Armour;
                _health = data.Value.Item2 as Health;
                return DataComponentRequestResult.Accept;
            }

            // Говорим провайдеру о том, что нужно подождать данные или отбросить модификатор.
            return DataComponentRequestResult.WaitForData;
            return DataComponentRequestResult.Discard;
        }

        public override void Dispose()
        {
            _health = null;
            _armour = null;
        }

        public override void Update(float deltaTime)
        {
            // Здесь мы не должны бояться пустого значения,
            // т.к. провайдер должен был сам поставить модификатор в нужную очередь исполнения.  
            _armour.ArmourPoints++;
            _health.HealthPoints--;
        }
    }
}