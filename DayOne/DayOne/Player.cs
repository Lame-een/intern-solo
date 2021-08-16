using System;
using System.Collections.Generic;

namespace DayOne
{
    class Player : Entity, ITargetable
    {
        private List<Item> _equipmentList = new List<Item>();
        private double _armorRating = 0;

        public List<Item> Equipment { get => _equipmentList; }


        public Player(string name) : base(name)
        {
        }


        public override void DoAction(Attack attack, ITargetable target, ref bool escaped)
        {
            if(target.Health <= 0)
            {
                Console.WriteLine("Target is already dead.");
                return;
            }

            //probably better to have a Singelton that manages random generation
            var rand = new Random();
            Console.WriteLine($"{Name} is attacking {target.Name} with {attack.Name}.");

            if (rand.NextDouble() > attack.HitChance)
            {
                Console.WriteLine($"{Name} missed.");
            }
            else
            {
                target.TakeDamage(attack);
            }
        }

        public void TakeDamage(Attack attack)
        {
            int damageRecieved = attack.Damage - (int)(_armorRating / 3);
            Health -= damageRecieved;
            Health = Math.Max(0, Health);

            Console.WriteLine($"{Name} takes {damageRecieved} of damage.\n{Name} has {Health} health.");
            if(Health <= 0)
            {
                Console.WriteLine($"{Name} has died.");
            }
        }

        public override void Equip(Item item)
        {
            if (Equipment.Exists(x => (x.Type == item.Type)))
            {
                Console.WriteLine($"Item of that type already equipped.");
            }
            else
            {
                Console.WriteLine($"Player {Name} equips {item.Name}");
                _equipmentList.Add(item);
                _armorRating += item.Defense;
            }
        }
    }
}
