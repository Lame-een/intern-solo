using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01
{
    class Mob : Entity, ITargetable, IDeepCloneable<Mob>
    {
        public bool IsHostile { get; }

        public Mob(string name, int health = 100, bool isHostile = false) : base(name, health)
        {
            IsHostile = isHostile;
        }
        public override void DoAction(Attack attack, ITargetable target, ref bool escaped)
        {
            if(Health <= 0)
            {
                return;
            }
            if (target.Health <= 0)
            {
                Console.WriteLine("Target is already dead.");
                return;
            }

            var rand = new Random();
            if (!IsHostile)
            {
                if (rand.Next(1, 21) == 20)
                {
                    Console.WriteLine($"{Name} successfuly escaped.");
                    escaped = true;
                }
                return;
            }


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
            Health -= attack.Damage;
            Console.WriteLine($"{Name} takes {attack.Damage} of damage.");
            if (Health <= 0)
            {
                Console.WriteLine($"{Name} died.");
            }
        }

        public Mob DeepClone()
        {
            return new Mob(Name, Health, IsHostile);
        }
    }
}
