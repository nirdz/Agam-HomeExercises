using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil1
{
    public class Egg : Product
    {
        public override string Name { get; } = "Egg";
        public override double Price { get; } = 0.8;
        public override string CatalogCode { get; } = "Eggs4A";

        public Egg(string identifierCode, DateTime expirationDate)
        {
            IdentifierCode = identifierCode;
            ExpirationDate = expirationDate;
        }


        /* For getting the catalog code of the product class without creating it */

        private Egg() { }

        public static string GetCatalogCode()
        {
            return new Egg().CatalogCode;
        }
    }
}
