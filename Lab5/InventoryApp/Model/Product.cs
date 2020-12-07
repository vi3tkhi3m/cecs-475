using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace InventoryApp.Model
{
    [Serializable]
    public class Product
    {
        public int ProductId {get; set;}
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }

        public Product() { }

        public Product(int productId, string productName, int productQuantity)
        {
            ProductId = productId;
            ProductName = productName;
            ProductQuantity = productQuantity;
        }
    }
}
