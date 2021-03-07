namespace Modifiers
{
    // Комментарии в ArmourRaiser. !!!!!!!!!!!!!!
    
    public class HealthDoubler : DataComponentModifier
    {
        private Health _health;
        
        public override DataComponentRequestResult RequestDataComponents(IDataComponentProvider provider)
        {
            // Здесь сразу возвращается значение без ValueTuple.
            var data = provider.RequestComponent<Health>();
            
            if(data.IsSome)
            {
                _health = data.Value as Health;
                return DataComponentRequestResult.Accept;
            }

            return DataComponentRequestResult.WaitForData;
            return DataComponentRequestResult.Discard;
        }

        public override void Dispose()
        {
            _health = null;
        }

        public override void Update(float deltaTime)
        {
            // Здесь мы не должны бояться пустого значения,
            // т.к. провайдер должен был сам поставить модификатор в нужную очередь исполнения.  
            _health.HealthPoints++;
        }
    }
}