using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil1
{
    public class Banana : Product
    {
        public override string Name { get; } = "Banana";
        public override double Price { get; } = 1.2;
        public override string CatalogCode { get; } = "Bananas1A";

        public Banana(string identifierCode, DateTime expirationDate)
        {
            IdentifierCode = identifierCode;
            ExpirationDate = expirationDate;
        }


        /* For getting the catalog code of the product class without creating it */
         
        private Banana() { }

        public static string GetCatalogCode()
        {
            return new Banana().CatalogCode;
        }
    }
}
