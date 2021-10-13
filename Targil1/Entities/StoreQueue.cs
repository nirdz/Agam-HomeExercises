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
                throw new PersonNotAllowedToEnterStoreException(RejectionReason.BodyHeatHigh);
            }
            if (!cus.IsWearingMask)
            {
                throw new PersonNotAllowedToEnterStoreException(RejectionReason.NoMask);
            }
            if (cus.IsInIsolation)
            {
                throw new PersonNotAllowedToEnterStoreException(RejectionReason.InIsolation);
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
         *   
        */
        public void PrintCustomersInQueue()
        {
            foreach (Customer cus in customerQueue)
            {
                Console.WriteLine(cus);
            }
            Console.WriteLine($"Total number of customers in queue: {customerQueue.Count}");
        }

        public int GetCountCustomersInQueue()
        {
            return customerQueue.Count;
        }
    }


}
