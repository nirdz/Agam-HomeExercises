using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil1
{
    public class Banana : Product
    {
        public override string Name { get => "Banana"; }
        public override double Price { get => 1.2; }
        public override string CatalogCode { get => "1A"; }

        public Banana(string identifierCode, DateTime expirationDate)
        {
            IdentifierCode = identifierCode;
            ExpirationDate = expirationDate;
        }
    }
}
