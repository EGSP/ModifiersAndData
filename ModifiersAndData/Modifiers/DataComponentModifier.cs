namespace Modifiers
{
    public abstract class DataComponentModifier
    {
        /// <summary>
        /// Вызывается извне провайдером. Этот метод вызывается до первого обновления.
        /// </summary>
        public abstract DataComponentRequestResult RequestDataComponents(IDataComponentProvider provider);
        
        public abstract void Dispose();

        public abstract void Update(float deltaTime);
    }
    
    public enum DataComponentRequestResult
    {
        Accept,
        Discard,
        WaitForData
    }
}