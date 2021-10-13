using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil1
{
    /*
     *  Represents a product in the store.
     *  Each single product is uniquely identified by "IdentifierCode",
     *  and belongs to a catalog of products. The catalog code is "CatalogCode"
    */
    public abstract class Product
    {
        public abstract string Name { get; }
        public abstract double Price { get; }
        public abstract string CatalogCode { get; }
        public string IdentifierCode { get; set; }
        public DateTime ExpirationDate { get; set; }


        public override bool Equals(object obj)
        {
            var product = obj as Product;
            return product != null &&
                   IdentifierCode == product.IdentifierCode;
        }

        public override int GetHashCode()
        {
            return IdentifierCode.GetHashCode();
        }

        public override string ToString()
        {
            return $"Name: {Name}, Price: {Price}, Expiration Date: {ExpirationDate}" 
                + $", Catalog Code: {CatalogCode}, Identifier Code: {IdentifierCode}";
        }
    }
}
