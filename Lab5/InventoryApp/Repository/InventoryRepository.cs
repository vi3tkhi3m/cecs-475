using GalaSoft.MvvmLight;
using InventoryApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace InventoryApp.Repository
{
    public class InventoryRepository : IInventoryRepository
    {
        /// <summary>
        /// The list of products to be saved.
        /// </summary>
        private ObservableCollection<Product> products;

        private const string FILE_PATH = "./products.txt";

        public InventoryRepository()
        {
            products = new ObservableCollection<Product>();
        }

        public ObservableCollection<Product> GetProducts()
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<Product>));
                using (StreamReader rd = new StreamReader(FILE_PATH))
                {
                    products = xs.Deserialize(rd) as ObservableCollection<Product>;
                }
                SortProductListById();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found");
            }
            return products;
        }

        public void SaveProduct(Product product)
        {
            products.Add(product);
            SortProductListById();
            SaveProductListToFile();
        }

        public bool DoesProductIdExist(int productId)
        {
            return products.Any(u => u.ProductId == productId); ;
        }

        private void SaveProductListToFile()
        {
            XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<Product>));
            using (StreamWriter wr = new StreamWriter(FILE_PATH))
            {
                xs.Serialize(wr, products);
            }
        }

        private void SortProductListById()
        {
            products = new ObservableCollection<Product>(products.OrderBy(i => i.ProductId));
        }

        public void EditProduct(int currentproduct, Product product)
        {
            Product p = products.Single(i => i.ProductId == currentproduct);
            p.ProductId = product.ProductId;
            p.ProductName = product.ProductName;
            p.ProductQuantity = product.ProductQuantity;
            SortProductListById();
            SaveProductListToFile();
        }

        public void DeleteProduct(int productId)
        {
            Product p = products.Single(i => i.ProductId == productId);
            products.Remove(p);
            SortProductListById();
            SaveProductListToFile();
        }
    }
}
