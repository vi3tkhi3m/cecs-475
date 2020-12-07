using InventoryApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryApp.Repository
{
    public interface IInventoryRepository
    {
        ObservableCollection<Product> GetProducts();

        void SaveProduct(Product product);

        void EditProduct(int currentproduct, Product product);

        void DeleteProduct(int productId);

        bool DoesProductIdExist(int productId);
    }
}
