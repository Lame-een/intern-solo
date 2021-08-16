using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOne
{
    class Attack
    {
        private Item _attackItem;
        public Item ItemUsed { get; }
        public string Name { get;}
        public int Damage { get; set; }
        public double HitChance { get; }

        public Attack(string name, int damage, double hitChance)
        {
            ItemUsed = Globals.NullItem; //circumventing C# version limitation
            Name = name;
            Damage = damage;
            HitChance = hitChance;
        }

        public Attack(Item item, double hitChance)
        {
            ItemUsed = item;
            Name = item.Name;
            Damage = item.Damage;
            HitChance = hitChance;
        }

        public static void ChangeItem(Attack attack, Item item)
        {
            attack._attackItem = item;
        }

    }
}
