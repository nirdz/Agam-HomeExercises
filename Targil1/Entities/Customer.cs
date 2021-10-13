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


        /*
         *  Returns whether a customer can enter the store's queue.
         *  A customer can enter if his body heat <= 38, 
         *  wears mask and is not in inIsolation.
         */
        /*public bool CanEnterStoreQueue()
        {
            if(BodyHeat <= 38)
        }*/
    }
}
