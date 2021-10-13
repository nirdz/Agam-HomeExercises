using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil1
{
    /*
     *  Helps to dynamically create products. 
    */
    public class ProductFactory
    {
        /*
         *   Dynamically create products. the type of the product is determined by "prodName"
        */
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
