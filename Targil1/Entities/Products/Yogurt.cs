using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil1
{
    public class Yogurt : Product
    {
        public override string Name { get => "Yogurt"; }
        public override double Price { get => 4.5; }
        public override string CatalogCode { get => "5B"; }

        public Yogurt(string identifierCode, DateTime expirationDate)
        {
            IdentifierCode = identifierCode;
            ExpirationDate = expirationDate;
        }
    }
}
