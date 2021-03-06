using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil1
{
    public class Bread : Product
    {
        public override string Name { get; } = "Bread";
        public override double Price { get; } = 4.9;
        public override string CatalogCode { get; } = "Breads3A";

        public Bread(string identifierCode, DateTime expirationDate)
        {
            IdentifierCode = identifierCode;
            ExpirationDate = expirationDate;
        }


        /* For getting the catalog code of the product class without creating it */

        private Bread() { }

        public static string GetCatalogCode()
        {
            return new Bread().CatalogCode;
        }
    }
}
