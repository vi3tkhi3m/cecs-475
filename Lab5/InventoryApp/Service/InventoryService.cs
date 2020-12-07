using GalaSoft.MvvmLight;
using InventoryApp.Model;
using InventoryApp.Repository;
using InventoryApp.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryApp.Service
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public void EditProduct(int currentproduct, Product product)
        {
            _inventoryRepository.EditProduct(currentproduct, product);
        }

        public ObservableCollection<Product> GetProducts()
        {
            return _inventoryRepository.GetProducts();
        }

        /// <summary>
        /// Checks if the entered product (Add) is valid.
        /// </summary>
        /// <returns>
        /// If product is valid return true, else throw.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown if the ID already exists or field contains too many characters</exception>+
        /// <exception cref="NullReferenceException">Thrown If the product name is empty</exception>
        /// <exception cref="FormatException">Thrown if the quantity is not in range</exception>
        /// <param name="product">The product that needs to be validated.</param>
        public bool IsProductValid(Product product)
        {
            if (_inventoryRepository.DoesProductIdExist(product.ProductId))
            {
                throw new ArgumentException("Id already exists!");
            }

            GeneralProductValidation(product);

            return true;
        }

        /// <summary>
        /// Checks if the entered (edit) product is valid. 
        /// </summary>
        /// <returns>
        /// If product is valid return true, else throw.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown if the ID already exists or field contains too many characters</exception>+
        /// <exception cref="NullReferenceException">Thrown If the product name is empty</exception>
        /// <exception cref="FormatException">Thrown if the quantity is not in range</exception>
        /// <param name="currentProductId">The id of the product that's being edited.</param>
        /// <param name="editProduct">The edited product.</param>
        public bool IsProductValid(int currentProductId, Product editProduct)
        {
            if (currentProductId != editProduct.ProductId)
            {
                if (_inventoryRepository.DoesProductIdExist(editProduct.ProductId))
                {
                    throw new ArgumentException("Id already exists!");
                }
            }

            GeneralProductValidation(editProduct);

            return true;
        }

        private void GeneralProductValidation(Product product)
        {
            if (string.IsNullOrEmpty(product.ProductName))
            {
                throw new NullReferenceException();
            }

            if (product.ProductName.Length > Globals.TEXT_LIMIT)
            {
                throw new ArgumentException($"Fields must be under {Globals.TEXT_LIMIT} characters.");
            }

            if (product.ProductQuantity < Globals.PRODUCT_MIN_QUANTITY || product.ProductQuantity > Globals.PRODUCT_MAX_QUANTITY)
            {
                throw new FormatException();
            }
        }

        public void SaveProduct(Product product)
        {
            _inventoryRepository.SaveProduct(product);
        }

        public void DeleteProduct(int productId)
        {
            _inventoryRepository.DeleteProduct(productId);
        }
    }
}
