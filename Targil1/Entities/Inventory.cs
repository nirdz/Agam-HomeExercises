using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil1
{

    public class Inventory
    {
        private Dictionary<string, ProductCatalog> productCatalogs;
        public Dictionary<string, Product> AllProducts { get; }

        public Inventory()
        {
            productCatalogs = new Dictionary<string, ProductCatalog>();
            AllProducts = new Dictionary<string, Product>();
        }

        public void AddCatalog(string catalogCode)
        {
            ProductCatalog productCatalog = new ProductCatalog(catalogCode);
            productCatalogs.Add(catalogCode, productCatalog);
        }

        public void AddCatalog(string catalogCode, HashSet<Product> products)
        {
            ProductCatalog productCatalog = new ProductCatalog(catalogCode, products);
            productCatalogs.Add(catalogCode, productCatalog);
            // Add to allProducts dict
            foreach (Product p in products)
            {
                AllProducts.Add(p.IdentifierCode, p);
            }
        }

        public void AddProduct(Product p)
        {
            string catalogCode = p.CatalogCode;
            if (!productCatalogs.ContainsKey(catalogCode))
            {
                throw new CatalogNotExistsException($"Catalog code {catalogCode} not exists in inventory");
            }
            productCatalogs[catalogCode].AddProduct(p);
            AllProducts.Add(p.IdentifierCode, p);
        }

        public Product GetProductByIdentifierCode(string identifierCode)
        {
            if (AllProducts.ContainsKey(identifierCode))
            {
                return AllProducts[identifierCode];
            }
            return null;
        }

        public void RemoveProduct(Product p)
        {
            string catalogCode = p.CatalogCode;
            if (!productCatalogs.ContainsKey(catalogCode))
            {
                throw new CatalogNotExistsException($"Catalog code {catalogCode} not exists in inventory");
            }
            ProductCatalog productCatalog = productCatalogs[catalogCode];
            bool isRemoved = productCatalog.RemoveProduct(p);
            if (isRemoved)
            {
                AllProducts.Remove(p.IdentifierCode);
                // Check if the catalog has less than 20 items,
                int numProductsInCatalog = productCatalog.GetNumProducts();
                if(numProductsInCatalog < 20)
                {
                    OrderStatus status = productCatalog.OrderStatus;
                    // Check if there is no order for the product
                    if (status.Equals(OrderStatus.NoOrder)){
                        Console.WriteLine($"There are less than 20 products in catalog code {catalogCode}" + 
                        ". It is recommended to order more");
                    }
                    else
                    {
                        // There is already an order
                        Console.WriteLine($"There are less than 20 products in catalog code {catalogCode}" +
                        ". There is already an order with status " + status);
                    }
                    
                }
            }
        }

        public void PrintInventory()
        {
            int totalProductsInInventory = 0;
            // Sort ProductCatalogs by OrderStatus
            var sortedProductCatalogs = productCatalogs.ToList();
            sortedProductCatalogs.Sort((pair1, pair2) => pair1.Value.OrderStatus.CompareTo(pair2.Value.OrderStatus));

            foreach (var pair in sortedProductCatalogs)
            {
                ProductCatalog productCatalog = pair.Value;
                Console.WriteLine(productCatalog + "\n");
                totalProductsInInventory += productCatalog.GetNumProducts();
            }
            Console.WriteLine($"Total number of products in inventory: {totalProductsInInventory}");
        }

        private class ProductCatalog
        {
            public string CatalogCode { get; }
            public HashSet<Product> Products { get; }
            public OrderStatus OrderStatus { get; set; }

            public ProductCatalog(string catalogCode)
            {
                CatalogCode = catalogCode;
                Products = new HashSet<Product>();
                OrderStatus = OrderStatus.NoOrder;
            }

            public ProductCatalog(string catalogCode, HashSet<Product> products)
            {
                // Verify that all products are from this catalog
                foreach (Product p in products)
                {
                    if (!p.CatalogCode.Equals(catalogCode))
                    {
                        throw new Exception("One of the products has a different catalog code");
                    }
                }
                CatalogCode = catalogCode;
                Products = products;
                OrderStatus = OrderStatus.NoOrder;
            }

            public void AddProduct(Product p)
            {
                if (!p.CatalogCode.Equals(CatalogCode))
                {
                    throw new Exception("Could not add product with catalog code " + p.CatalogCode +
                        " to different catalog (" + CatalogCode + ")");
                }
                Products.Add(p);
            }

            public bool RemoveProduct(Product p)
            {
                return Products.Remove(p);
            }

            public int GetNumProducts()
            {
                return Products.Count;
            }

            public override string ToString()
            {
                string str = "Catalog Code: " + CatalogCode + ",\nOrder Status: " + OrderStatus + ",\nProducts: ";
                foreach (Product p in Products)
                {
                    str += p + "\n";
                }
                str += "Total number of products in catalog: " + Products.Count;
                return str;
            }
        }

    }
}
