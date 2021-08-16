using System;

namespace DayOne
{
    class Entity
    {
        public string Name { get; }
        public int Health { get; set; }

        public Entity(string name = "Unnamed", int health = 100)
        {
            Name = name;
            Health = health;
        }

        public virtual void DoAction(Attack attack, ITargetable target, ref bool escaped)
        {
            Console.WriteLine($"{Name} cannot do an action.");
        }

        public virtual void Equip(Item item)
        {
            Console.WriteLine($"{Name} cannot equip an item.");
        }
    }
}
