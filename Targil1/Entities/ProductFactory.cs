using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil1
{
    public class ProductFactory
    {
        public static Product CreateProduct(string prodName, string identifierCode, DateTime expirationDate)
        {
            Product prod = null;
            switch (prodName.ToLower())
            {
                case "banana":
                    prod = new Banana(identifierCode, expirationDate);
                    break;
                case "milk":
                    prod = new Milk(identifierCode, expirationDate);
                    break;
                case "bread":
                    prod = new Bread(identifierCode, expirationDate);
                    break;
                case "egg":
                    prod = new Egg(identifierCode, expirationDate);
                    break;
                case "yogurt":
                    prod = new Yogurt(identifierCode, expirationDate);
                    break;
                case "oil":
                    prod = new Oil(identifierCode, expirationDate);
                    break;
                default:
                    break;
            }
            return prod;
        }
    }
}
