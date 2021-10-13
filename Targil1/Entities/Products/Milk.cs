using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil1
{
    public class Milk : Product
    {
        public override string Name { get; } = "Milk";
        public override double Price { get; } = 5.4;
        public override string CatalogCode { get;  } = "Milks2D5";

        public Milk(string identifierCode, DateTime expirationDate)
        {
            IdentifierCode = identifierCode;
            ExpirationDate = expirationDate;
        }

        /* For getting the catalog code of the product class without creating  it*/

        private Milk() { }

        public static string GetCatalogCode()
        {
            return new Milk().CatalogCode;
        }
    }
}
