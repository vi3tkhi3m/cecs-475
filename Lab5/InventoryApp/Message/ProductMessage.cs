using InventoryApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryApp.Message
{
    public class ProductMessage : Product
    {
        /// <summary>
        /// A property that includes the message.
        /// </summary>
        public string Message { get; private set; }

        public ProductMessage(int productId, string productName, int productQuantity, string message) :
            base(productId, productName, productQuantity)
        {
            Message = message;
        }

        public ProductMessage(Product product, string message) :
            base(product.ProductId, product.ProductName, product.ProductQuantity)
        {
            Message = message;
        }
    }
}
