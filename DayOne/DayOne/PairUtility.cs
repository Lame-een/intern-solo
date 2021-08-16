using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOne
{
    //cheeky way to do the "generics" task
    class PairUtility<K, V> : IDeepCloneable<PairUtility<K, V>> 
        where K : IDeepCloneable<K>
    {
        public K First;
        public List<V> Second;

        public PairUtility(K first, List<V> second)
        {
            First = first.DeepClone();
            Second = new List<V>(second);
        }

        public PairUtility<K, V> DeepClone()
        {
            return new PairUtility<K, V>(First, Second);
        }

        public V RandomSecond()
        {
            return Globals.RandomListElement<V>(Second);
        }
    }
}
