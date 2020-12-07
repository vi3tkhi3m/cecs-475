using InventoryApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryApp.Service
{
    public interface IInventoryService
    {
        ObservableCollection<Product> GetProducts();

        void SaveProduct(Product product);

        void EditProduct(int currentProductId, Product editProduct);

        void DeleteProduct(int productId);

        bool IsProductValid(Product product);

        bool IsProductValid(int productId, Product product);
    }
}
