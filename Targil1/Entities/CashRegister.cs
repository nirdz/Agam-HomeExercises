using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil1
{
    public class CashRegister
    {
        /*
         *  Log of when each worker activated the cashier.
         *  List of pairs - id of worker, date of activation.
        */
        public List<KeyValuePair<int, DateTime>> ActivationLog { get; }

        /*
         *  Log of products bought by customers at the cashier.
         *  List of pairs - id of customer, list of products bought by the customer.
        */
        public List<KeyValuePair<int, List<Product>>> ShoppingLog { get; }

        public CashRegister()
        {
            ActivationLog = new List<KeyValuePair<int, DateTime>>();
            ShoppingLog = new List<KeyValuePair<int, List<Product>>>();
        }

        /*
         *  Logs the activation of the cash register by worker  
        */ 
        public void ActivateCashRegisterByWorker(int workerId, DateTime date)
        {
            ActivationLog.Add(new KeyValuePair<int, DateTime>(workerId, date));
        }

        /*
         *  Logs the transaction made by a customer
        */
        public void RegisterCustomerTransaction(int customerId, List<Product> productsBought)
        {
            ShoppingLog.Add(new KeyValuePair<int, List<Product>>(customerId, productsBought));
        }

        public void PrintActivationLog()
        {
            foreach (KeyValuePair<int, DateTime> record in ActivationLog)
            {
                Console.WriteLine($"Worker id: {record.Key}, Time: {record.Value}");
            }
            Console.WriteLine($"Total records: {ActivationLog.Count}");
        }
    }
}
