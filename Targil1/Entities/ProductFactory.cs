using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil1
{
    public class ProductFactory
    {
        public Product CreateProduct(string prodName, string identifierCode, DateTime expirationDate)
        {
            Product prod = null;
            switch (prodName)
            {
                case "Banana":
                    prod = new Banana(identifierCode, expirationDate);
                    break;
                case "Milk":
                    prod = new Milk(identifierCode, expirationDate);
                    break;
                case "Bread":
                    prod = new Bread(identifierCode, expirationDate);
                    break;
                case "Egg":
                    prod = new Egg(identifierCode, expirationDate);
                    break;
                case "Yogurt":
                    prod = new Yogurt(identifierCode, expirationDate);
                    break;
                case "Oil":
                    prod = new Oil(identifierCode, expirationDate);
                    break;
                default:
                    break;
            }
            return prod;
        }
    }
}
