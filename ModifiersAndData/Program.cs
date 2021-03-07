using System;

namespace Modifiers
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    // Здесь ничего интересного. Все самое ценное в Entity и ArmourRaiser.
    
    public sealed class Orc : Entity
    {
    }

    public sealed class Human : Entity
    {
    }

    public abstract class DataComponent
    {
        
    }

    public sealed class Health : DataComponent
    {
        public int HealthPoints { get; set; }
    }

    public sealed class Armour : DataComponent
    {
        public int ArmourPoints { get; set; }
    }
}