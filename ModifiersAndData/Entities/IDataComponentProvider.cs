using System;

namespace Modifiers
{
    public interface IDataComponentProvider
    {
        ModifierAddResult AddModifier(DataComponentModifier modifier);
        
        Option<DataComponent> RequestComponent<T>()
            where T : DataComponent;

        Option<ValueTuple<DataComponent, DataComponent>> RequestComponent<T, TU>()
            where T : DataComponent
            where TU : DataComponent;

        Option<ValueTuple<DataComponent, DataComponent, DataComponent>> RequestComponent<T, TU, TA>()
            where T : DataComponent
            where TU : DataComponent
            where TA : DataComponent;
    }
    
    public enum ModifierAddResult
    {
        Added,
        Discarded
    }

}