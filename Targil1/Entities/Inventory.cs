using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil1
{

    public class Inventory
    {
        private Dictionary<string, ProductCatalog> ProductCatalogs { get; }

        public Inventory()
        {
            ProductCatalogs = new Dictionary<string, ProductCatalog>();
        }

        public void AddCatalog(string catalogCode)
        {
            ProductCatalog productCatalog = new ProductCatalog(catalogCode);
            ProductCatalogs.Add(catalogCode, productCatalog);
        }

        public void AddCatalog(string catalogCode, HashSet<Product> products)
        {
            ProductCatalog productCatalog = new ProductCatalog(catalogCode, products);
            ProductCatalogs.Add(catalogCode, productCatalog);
        }

        public void AddProductToCatalog(string catalogCode, Product p)
        {
            if (!ProductCatalogs.ContainsKey(catalogCode))
            {
                throw new Exception("Catalog code " + catalogCode + " not exists in inventory");
            }
            ProductCatalogs[catalogCode].AddProduct(p);
        }

        public void RemoveProductFromCatalog(string catalogCode, Product p)
        {
            if (!ProductCatalogs.ContainsKey(catalogCode))
            {
                throw new Exception("Catalog code " + catalogCode + " not exists in inventory");
            }
            ProductCatalog productCatalog = ProductCatalogs[catalogCode];
            bool isRemoved = productCatalog.RemoveProduct(p);
            if (isRemoved)
            {
                // TODO: Check if the catalog has less than 20 items,
                int numProductsInCatalog = productCatalog.GetNumProducts();

            }
        }

        public void PrintInventory()
        {
            int totalProductsInInventory = 0;
            // Sort ProductCatalogs by OrderStatus
            var sortedProductCatalogs = ProductCatalogs.ToList();
            sortedProductCatalogs.Sort((pair1, pair2) => pair1.Value.OrderStatus.CompareTo(pair2.Value.OrderStatus));

            foreach (var pair in sortedProductCatalogs)
            {
                ProductCatalog productCatalog = pair.Value;
                Console.WriteLine(productCatalog);
                totalProductsInInventory += productCatalog.GetNumProducts();
            }
            Console.WriteLine("Total number of products in inventory: " + totalProductsInInventory);
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
                string str = "Catalog Code: " + CatalogCode + ", Order Status: " + OrderStatus + "Products: ";
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
