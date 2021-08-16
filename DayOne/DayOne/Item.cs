using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOne
{
    enum ItemType : byte
    {
        Helmet = 1,
        Chestplate,
        Leggings,
        Boots,
        Shield,
        Weapon,
        Utility,
        Other
    }
    class Item : IDeepCloneable<Item>
    {
        public string Name { get; protected set; }

        public ItemType Type { get; }

        public int Damage { get; protected set; }
        public int Defense { get; protected set; }

        public Item(string name, ItemType type, int damage = 0, int defense = 0)
        {
            Name = name;
            Type = type;
            Damage = damage;
            Defense = defense;
        }

        public static void EnchantItem(ref Item item)
        {
            Console.WriteLine($"Item {item.Name} has been enchanted!");
            item.Name = "Enchanted " + item.Name;
            item.Damage *= 2;
            item.Defense *= 2;
        }

        public Item DeepClone()
        {
            return new Item(Name, Type, Damage, Defense);
        }
    }
}
