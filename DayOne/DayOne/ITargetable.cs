using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayOne
{
    interface ITargetable
    {
        int Health { get; }
        string Name { get; }
        void TakeDamage(Attack attack);
    }
}
