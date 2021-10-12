using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil1
{
    public class Oil : Product
    {
        public override string Name { get => "Oil"; }
        public override double Price { get => 7.2; }
        public override string CatalogCode { get => "6B"; }

        public Oil(string identifierCode, DateTime expirationDate)
        {
            IdentifierCode = identifierCode;
            ExpirationDate = expirationDate;
        }
    }
}
