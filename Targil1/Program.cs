using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil1
{
    public class Program
    {
        public static Dictionary<int, Worker> storeWorkers;
        public static Inventory inventory;
        public static StoreQueue storeQueue;
        public static Dictionary<int, Customer> customersInsideStore;
        public static CashRegister cashRegister;
        public static WorkersManager workersManager;
        public static void Main(string[] args)
        {
            // Initiate store
            InitWorkers();
            InitInventory();
            storeQueue = new StoreQueue();
            customersInsideStore = new Dictionary<int, Customer>();
            cashRegister = new CashRegister();
            workersManager = new WorkersManager();

            // Show main menu 
            bool showMenu = true;
            while (showMenu)
            {
                Console.Clear();
                Console.WriteLine("Main Menu:");
                Console.WriteLine("1) Manage Queue");
                Console.WriteLine("2) Manage Cash Register");
                Console.WriteLine("3) Manage Workers");
                Console.WriteLine("4) Manage Inventory");
                Console.WriteLine("5) Exit");
                Console.Write("\r\nSelect an option: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        ManageQueuMenu();
                        break;
                    case "2":
                        ManageCashRegisterMenu();
                        break;
                    case "3":
                        ManageWorkersMenu();
                        break;
                    case "4":
                        ManageInventoryMenu();
                        break;
                    case "5":
                        showMenu = false;
                        break;
                    default:
                        break;
                }
            }
        }


        /* Manage Queue */

        private static void ManageQueuMenu()
        {
            bool showMenu = true;
            while (showMenu)
            {
                Console.Clear();
                Console.WriteLine("Manage Queue Menu:");
                Console.WriteLine("1) Insert Customer to Queue");
                Console.WriteLine("2) Insert Customers to Store");
                Console.WriteLine("3) Show Customers in Queue");
                Console.WriteLine("4) Back to Main Menu");
                Console.Write("\r\nSelect an option: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine();
                        InsertCustomerToQueue();
                        break;
                    case "2":
                        Console.WriteLine();
                        InsertCustomersToStore();
                        break;
                    case "3":
                        Console.WriteLine();
                        ShowCustomersInQueue();
                        break;
                    case "4":
                        return;
                    default:
                        break;
                }
            }
        }

        private static void InsertCustomerToQueue()
        {
            Console.WriteLine("Enter customer's id:");
            int id = GetValidIntInput();
            Console.WriteLine("Enter customer's body temperature:");
            double bodyHeat = GetValidDoubleInput();
            Console.WriteLine("Is the customer wears mask? (y/n):");
            bool isWearingMask = GetValidBoolInput();
            Console.WriteLine("Is the customer shpuld be in isolation? (y/n):");
            bool isInIsolation = GetValidBoolInput();
            Customer customer = new Customer(id, bodyHeat, isWearingMask, isInIsolation);
            try
            {
                storeQueue.EnqueueCustomer(customer);
                Console.WriteLine("Customer added to queue");
            }
            catch(PersonNotAllowedToEnterStoreException e)
            {
                string exceptionMsg = "Customer cannot enter queue because ";
                switch (e.RejectionReason)
                {
                    case RejectionReason.BodyHeatHigh:
                        exceptionMsg += $"his body temperature is over 38 ({customer.BodyHeat})";
                        break;
                    case RejectionReason.NoMask:
                        exceptionMsg += "he is not wearing mask";
                        break;
                    case RejectionReason.InIsolation:
                        exceptionMsg += "he should be in isolation";
                        break;
                    default:
                        break;
                }
                Console.WriteLine(exceptionMsg);
            }
            Console.Write("\r\nPress Enter to continue");
            Console.ReadLine();
        }

        private static void InsertCustomersToStore()
        {
            Console.WriteLine("Enter how many customers to enter store (will be removed from queue):");
            int input = GetValidIntInput();
            int customersInQueue = storeQueue.GetCountCustomersInQueue();
            int customersToEnterStore = Math.Min(input, customersInQueue);
            for (int i = 0; i < customersToEnterStore; i++)
            {
                Customer cus = storeQueue.DequeueCustomer();
                customersInsideStore.Add(cus.Id, cus);
            }
            Console.WriteLine($"{customersToEnterStore} customers entered the store");
            Console.Write("\r\nPress Enter to continue");
            Console.ReadLine();
        }

        private static void ShowCustomersInQueue()
        {
            storeQueue.PrintCustomersInQueue();
            Console.Write("\r\nPress Enter to continue");
            Console.ReadLine();
        }


        /* Manage Cash Register */

        private static void ManageCashRegisterMenu()
        {
            bool showMenu = true;
            while (showMenu)
            {
                Console.Clear();
                Console.WriteLine("Manage Cash Register Menu:");
                Console.WriteLine("1) Activate Cash Register");
                Console.WriteLine("2) Register Customer's Transaction");
                Console.WriteLine("3) Show Cash Register Activations History");
                Console.WriteLine("4) Report Corona Carrier Customer");
                Console.WriteLine("5) Back to Main Menu");
                Console.Write("\r\nSelect an option: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine();
                        ActivateCashRegister();
                        break;
                    case "2":
                        Console.WriteLine();
                        RegisterCustomerTransaction();
                        break;
                    case "3":
                        Console.WriteLine();
                        ShowCashRegisterActivationHistory();
                        break;
                    case "4":
                        Console.WriteLine();
                        ReportCoronaCustomer();
                        break;
                    case "5":
                        return;
                    default:
                        break;
                }
            }
        }

        private static void ActivateCashRegister()
        {
            PrintStoreWorkers();
            Console.WriteLine();
            int workerId = GetValidWorkerIdInput();
            DateTime activationDate = DateTime.Now;
            cashRegister.ActivateCashRegisterByWorker(workerId, activationDate);
            Console.WriteLine("Activation registered in history");

            Console.Write("\r\nPress Enter to continue");
            Console.ReadLine();
        }

        private static void RegisterCustomerTransaction()
        {
            PrintCustomersInsideStore();
            int customerId = GetValidCustomerIdInput();
            Console.WriteLine();
            PrintStoreProducts();
            Console.WriteLine();
            Console.WriteLine("Enter identifier codes (barcode) of the customer's products.");
            Console.WriteLine("Press 0 to finish.");
            List<Product> productsBought = new List<Product>();
            string identifierCode = Console.ReadLine();
            while (!identifierCode.Equals("0"))
            {
                Product p = inventory.GetProductByIdentifierCode(identifierCode);
                if(p == null)
                {
                    Console.WriteLine($"Product with identifier code {identifierCode} not exists.");
                }
                productsBought.Add(p);
                identifierCode = Console.ReadLine();
            }
            Console.WriteLine();
            cashRegister.RegisterCustomerTransaction(customerId, productsBought);
            Console.WriteLine("Customer's transaction registered in history");
            Console.WriteLine();
            Console.WriteLine("Removing the products from inventory...");
            foreach (Product p in productsBought)
            {
                inventory.RemoveProduct(p);
            }
            Console.WriteLine();
            Console.WriteLine("products removed from inventory");

            Console.WriteLine();
            
            Console.Write("\r\nPress Enter to continue");
            Console.ReadLine();
        }

        private static void ShowCashRegisterActivationHistory()
        {
            cashRegister.PrintActivationLog();
            Console.Write("\r\nPress Enter to continue");
            Console.ReadLine();
        }

        private static void ReportCoronaCustomer()
        {
            Console.WriteLine("Enter id of the Corona carrier customer:");
            int infectedCustomerId = GetValidIntInput();
            /*Customer infectedCustomer = customersInsideStore[infectedCustomerId];
            PrintStoreWorkers();*/
            Console.WriteLine();
            Console.WriteLine("Enter ids of workers that were around this customer.");
            Console.WriteLine("Press 0 to finish.");
            int workerId = GetValidIntInput();
            while (workerId != 0)
            {
                /*bool isWorkerExists = storeWorkers.ContainsKey(workerId);
                while (!isWorkerExists)
                {
                    Console.WriteLine($"Worker with id {workerId} not found");
                    workerId = GetValidIntInput();
                    isWorkerExists = storeWorkers.ContainsKey(workerId);
                }
                Worker worker = storeWorkers[workerId];
                // Worker should be in isolation now
                worker.IsInIsolation = true;*/
                workerId = GetValidIntInput();
            }

            Console.WriteLine("Enter ids of customers that were around this customer.");
            Console.WriteLine("Press 0 to finish.");
            int customerId = GetValidIntInput();
            while (customerId != 0)
            {
                /*bool isCustomerExistsInsideStore = customersInsideStore.ContainsKey(customerId);
                while (!isCustomerExistsInsideStore)
                {
                    Console.WriteLine("Customer not found inside store. Enter customer's id:");
                    customerId = GetValidIntInput();
                    isCustomerExistsInsideStore = storeWorkers.ContainsKey(customerId);
                }
                Customer customer = customersInsideStore[customerId];
                // Customer should be in isolation now
                customer.IsInIsolation = true;*/
                customerId = GetValidIntInput();
            }
            Console.WriteLine();
            Console.WriteLine("The workers and customers around the infected customer should be in isolation");
            Console.Write("\r\nPress Enter to continue");
            Console.ReadLine();
        }


        /* Manage Workers */

        private static void ManageWorkersMenu()
        {
            bool showMenu = true;
            while (showMenu)
            {
                Console.Clear();
                Console.WriteLine("Manage Workers Menu:");
                Console.WriteLine("1) Clock In Worker");
                Console.WriteLine("2) Clock Out Worker");
                Console.WriteLine("3) Show Attendance Tracking History");
                Console.WriteLine("4) Back to Main Menu");
                Console.Write("\r\nSelect an option: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine();
                        ClockInWorker();
                        break;
                    case "2":
                        Console.WriteLine();
                        ClockOutWorker();
                        break;
                    case "3":
                        Console.WriteLine();
                        ShowAttendanceTrackingHistory();
                        break;
                    case "4":
                        return;
                    default:
                        break;
                }
            }
        }

        private static void ClockInWorker()
        {
            PrintStoreWorkers();
            Console.WriteLine();
            int workerId = GetValidWorkerIdInput();
            Worker worker = storeWorkers[workerId];
            DateTime clockInDate = DateTime.Now;
            try
            {
                workersManager.ClockInWorker(worker, clockInDate);
                Console.WriteLine("Attendance (clock-in) registered in history");
            }
            catch (PersonNotAllowedToEnterStoreException e)
            {
                string exceptionMsg = "Worker cannot clock in because ";
                switch (e.RejectionReason)
                {
                    case RejectionReason.BodyHeatHigh:
                        exceptionMsg += $"his body temperature is over 38 ({worker.BodyHeat}).";
                        break;
                    case RejectionReason.NoMask:
                        exceptionMsg += "he is not wearing mask.";
                        break;
                    case RejectionReason.InIsolation:
                        exceptionMsg += "he should be in isolation.";
                        break;
                    default:
                        break;
                }
                Console.WriteLine(exceptionMsg);
                Console.WriteLine("The worker is not allowed to work and should be fined 40 Shekels");
            }
            Console.Write("\r\nPress Enter to continue");
            Console.ReadLine();
        }

        private static void ClockOutWorker()
        {
            PrintStoreWorkers();
            Console.WriteLine();
            int workerId = GetValidWorkerIdInput();
            Worker worker = storeWorkers[workerId];
            DateTime clockOutDate = DateTime.Now;
            workersManager.ClockOutWorker(worker, clockOutDate);
            Console.WriteLine("Attendance (clock-out) registered in history");
            Console.Write("\r\nPress Enter to continue");
            Console.ReadLine();
        }

        private static void ShowAttendanceTrackingHistory()
        {
            workersManager.PrintAttendanceLog();
            Console.Write("\r\nPress Enter to continue");
            Console.ReadLine();
        }


        /* Manage Inventory */

        private static void ManageInventoryMenu()
        {
            bool showMenu = true;
            while (showMenu)
            {
                Console.Clear();
                Console.WriteLine("Manage Inventory Menu:");
                Console.WriteLine("1) Add Product to Inventory");
                Console.WriteLine("2) Print Inventory");
                Console.WriteLine("3) Back to Main Menu");
                Console.Write("\r\nSelect an option: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine();
                        AddProductToInventory();
                        break;
                    case "2":
                        Console.WriteLine();
                        PrintInventory();
                        break;
                    case "3":
                        return;
                    default:
                        break;
                }
            }
        }

        private static void AddProductToInventory()
        {
            Console.WriteLine("Enter product name (banana / bread / egg / milk / oil / yogurt):");
            string name = Console.ReadLine();
            Console.WriteLine("Enter product identifier code:");
            string IdentifierCode = Console.ReadLine();
            Console.WriteLine("Enter year of expiration date:");
            int year = GetValidIntInput();
            while(!(year > 1 && year < 9999))
            {
                Console.WriteLine("Please enter valid year:");
                year = GetValidIntInput();
            }
            Console.WriteLine("Enter month of expiration date:");
            int month = GetValidIntInput();
            while (!(month >= 1 && month <= 12))
            {
                Console.WriteLine("Please enter valid month:");
                month = GetValidIntInput();
            }
            DateTime expirationDate = new DateTime(year, month, 1);

            Console.WriteLine();
            Product p = ProductFactory.CreateProduct(name, IdentifierCode, expirationDate);
            if(p == null)
            {
                Console.WriteLine("Faild to create product. Please enter valid product name");
            }
            else
            {
                inventory.AddProduct(p);
                Console.WriteLine("Product added to inventory");
            }
            Console.Write("\r\nPress Enter to continue");
            Console.ReadLine();
        }

        private static void PrintInventory()
        {
            inventory.PrintInventory();
            Console.Write("\r\nPress Enter to continue");
            Console.ReadLine();
        }


        /* Initialize store */

        private static void InitWorkers()
        {
            Worker worker1 = new Worker(111, 36, true, false, WorkerRole.GeneralWorker);
            Worker worker2 = new Worker(222, 36, true, false, WorkerRole.GeneralWorker);
            Worker worker3 = new Worker(333, 36, true, false, WorkerRole.Cashier);
            Worker worker4 = new Worker(444, 40, true, false, WorkerRole.GeneralWorker);
            storeWorkers = new Dictionary<int, Worker>()
            {
                {worker1.Id, worker1 },
                {worker2.Id, worker2 },
                {worker3.Id, worker3 },
                {worker4.Id, worker4 },
            };
        }

        private static void InitInventory()
        {
            // Create products
            DateTime expirationDate1 = new DateTime(2021, 11, 20); // 20/11/2021
            DateTime expirationDate2 = new DateTime(2021, 6, 15);
            Product banana1 = ProductFactory.CreateProduct("Banana", "100", expirationDate1);
            Product banana2 = ProductFactory.CreateProduct("Banana", "101", expirationDate1);
            Product bread1 = ProductFactory.CreateProduct("Bread", "102", expirationDate1);
            Product bread2 = ProductFactory.CreateProduct("Bread", "103", expirationDate1);
            Product egg1 = ProductFactory.CreateProduct("Egg", "104", expirationDate2);
            Product egg2 = ProductFactory.CreateProduct("Egg", "105", expirationDate2);
            Product milk1 = ProductFactory.CreateProduct("Milk", "106", expirationDate2);
            Product milk2 = ProductFactory.CreateProduct("Milk", "107", expirationDate2);
            Product oil1 = ProductFactory.CreateProduct("Oil", "108", expirationDate2);
            Product oil2 = ProductFactory.CreateProduct("Oil", "109", expirationDate2);
            Product yogurt1 = ProductFactory.CreateProduct("Yogurt", "110", expirationDate2);
            Product yogurt2 = ProductFactory.CreateProduct("Yogurt", "111", expirationDate2);
            HashSet<Product> bananas = new HashSet<Product>() { banana1, banana2 };
            HashSet<Product> breads = new HashSet<Product>() { bread1, bread2 };
            HashSet<Product> eggs = new HashSet<Product>() { egg1, egg2 };
            HashSet<Product> milks = new HashSet<Product>() { milk1, milk2 };
            HashSet<Product> oils = new HashSet<Product>() { oil1, oil2 };
            HashSet<Product> yogurts = new HashSet<Product>() { yogurt1, yogurt2 };
            // Create inventory
            inventory = new Inventory();
            inventory.AddCatalog(Banana.GetCatalogCode(), bananas);
            inventory.AddCatalog(Bread.GetCatalogCode(), breads);
            inventory.AddCatalog(Egg.GetCatalogCode(), eggs);
            inventory.AddCatalog(Milk.GetCatalogCode(), milks);
            inventory.AddCatalog(Oil.GetCatalogCode(), oils);
            inventory.AddCatalog(Yogurt.GetCatalogCode(), yogurts);
        }


        /* Helper functions */

        private static int GetValidIntInput()
        {
            string input = Console.ReadLine();
            int num;
            while (!int.TryParse(input, out num))
            {
                Console.WriteLine("Please enter valid value (integer):");
                input = Console.ReadLine();
            }
            return num;
        }

        private static double GetValidDoubleInput()
        {
            string input = Console.ReadLine();
            double num;
            while (!double.TryParse(input, out num))
            {
                Console.WriteLine("Please enter valid value (number):");
                input = Console.ReadLine();
            }
            return num;
        }

        private static bool GetValidBoolInput()
        {
            string input = Console.ReadLine();
            while (!input.Equals("y") && !input.Equals("n"))
            {
                Console.WriteLine("Please enter valid value (y/n):");
                input = Console.ReadLine();
            }
            bool res = input.Equals("y");
            return res;
        }

        private static int GetValidWorkerIdInput()
        {
            Console.WriteLine("Enter workers's id:");
            int workerId = GetValidIntInput();
            bool isWorkerExists = storeWorkers.ContainsKey(workerId);
            while (!isWorkerExists)
            {
                Console.WriteLine("Worker not found. Enter workers's id:");
                workerId = GetValidIntInput();
                isWorkerExists = storeWorkers.ContainsKey(workerId);
            }
            return workerId;
        }

        private static int GetValidCustomerIdInput()
        {
            return 5;
            Console.WriteLine("Enter customer's id:");
            int customerId = GetValidIntInput();
            bool isCustomerExistsInsideStore = customersInsideStore.ContainsKey(customerId);
            while (!isCustomerExistsInsideStore)
            {
                Console.WriteLine("Customer not found inside store. Enter customer's id:");
                customerId = GetValidIntInput();
                isCustomerExistsInsideStore = storeWorkers.ContainsKey(customerId);
            }
            return customerId;
        }

        private static void PrintStoreWorkers()
        {
            Console.WriteLine("Store workers:");
            foreach (Worker worker in storeWorkers.Values)
            {
                Console.WriteLine($"Id: {worker.Id}, Role: {worker.Role}");
            }
        }

        private static void PrintCustomersInsideStore()
        {
            Console.WriteLine("Customers inside store:");
            foreach (Customer customer in customersInsideStore.Values)
            {
                Console.WriteLine($"Id: {customer.Id}");
            }
        }

        private static void PrintStoreProducts()
        {
            Console.WriteLine("Available products in store:");
            foreach (Product p in inventory.AllProducts.Values)
            {
                Console.WriteLine($"Identifier code: {p.IdentifierCode}, Name: {p.Name}, Price: {p.Price}");
            }
        }
    }
}
