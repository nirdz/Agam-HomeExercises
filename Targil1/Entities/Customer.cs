using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil1
{
    public class Customer : Person
    {
        public Customer(int id, double bodyHeat, bool isWearingMask, bool isInIsolation)
            :base(id, bodyHeat, isWearingMask, isInIsolation)
        {
        
        }

    }
}
