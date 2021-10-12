using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil1
{
    public class StoreQueue
    {
        private Queue<Customer> customerQueue;

        public StoreQueue()
        {
            customerQueue = new Queue<Customer>();
        }

        /*
         *  Inserts customer to customers' queue.
         *  Throws exception if the customer's body heat is over 38,
         *  is not wearing mask or should be in isolation.
         */
        public void EnqueueCustomer(Customer cus)
        {
            if(cus.BodyHeat > 38)
            {
                throw new CustomerNotAllowedToEnterQueueException(QueueRejectionReason.BodyHeatHigh);
            }
            if (!cus.IsWearingMask)
            {
                throw new CustomerNotAllowedToEnterQueueException(QueueRejectionReason.NoMask);
            }
            if (cus.IsInIsolation)
            {
                throw new CustomerNotAllowedToEnterQueueException(QueueRejectionReason.InIsolation);
            }
            customerQueue.Enqueue(cus);
        }

        /*
         *  Removes the first customer in the queue and returns it.
         */
        public Customer DequeueCustomer()
        {
            if(customerQueue.Count == 0)
            {
                return null;
            }
            Customer dequeuedCus = customerQueue.Dequeue();
            return dequeuedCus;
        }

        /*
         *  Returns how many customers are currently in the queue.  
         */
        public int GetNumCustomersInQueue()
        {
            return customerQueue.Count;
        }
    }


}
