using System;
using System.Collections.Generic;

namespace DayOne
{
    static class Globals
    {
        public static Item NullItem = new Item("null", ItemType.Other, 0, 0);
        public static Attack NullAttack = new Attack(NullItem, 0);

        public static T RandomArrayElement<T>(T[] arr)
        {
            if (arr.Length == 0)
            {
                throw new InvalidOperationException("Array contains no elements.");
            }

            var rand = new Random();
            return arr[rand.Next(0, arr.Length)];
        }

        public static T RandomListElement<T>(List<T> arr)
        {
            if (arr.Count == 0)
            {
                throw new InvalidOperationException("Array contains no elements.");
            }

            var rand = new Random();
            return arr[rand.Next(0, arr.Count)];
        }
    }
}
