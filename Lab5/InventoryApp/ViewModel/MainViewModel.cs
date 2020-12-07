using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using InventoryApp.Message;
using InventoryApp.Model;
using InventoryApp.Repository;
using InventoryApp.Service;
using InventoryApp.Util;
using InventoryApp.View;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace InventoryApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IInventoryService _inventoryService;

        private Product selectedProduct;

        public ICommand AddCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }

        public MainViewModel(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
            AddCommand = new RelayCommand(AddMethod);
            ExitCommand = new RelayCommand<IClosable>(ExitMethod);
            EditCommand = new RelayCommand(EditMethod);
            Messenger.Default.Register<ProductMessage>(this, ReceiveProduct);
            Messenger.Default.Register<NotificationMessage>(this, ReceiveMessage);
        }

        /// <summary>
        /// The currently selected product in the list box.
        /// </summary>
        public Product SelectedProduct
        {
            get
            {
                return selectedProduct;
            }
            set
            {
                selectedProduct = value;
                RaisePropertyChanged("SelectedProduct");
            }
        }

        /// <summary>
        /// Shows a new add screen.
        /// </summary>
        public void AddMethod()
        {
            AddWindow add = new AddWindow();
            add.Show();
        }

        /// <summary>
        /// Closes the application.
        /// </summary>
        /// <param name="window">The window to close.</param>
        public void ExitMethod(IClosable window)
        {
            if (window != null)
            {
                window.Close();
            }
        }
        /// <summary>
        /// Opens the edit window.
        /// </summary>
        public void EditMethod()
        {
            if (SelectedProduct != null)
            {
                EditWindow edit = new EditWindow();
                edit.Show();
                Messenger.Default.Send(selectedProduct);
            }
        }
        /// <summary>
        /// Gets a new product for the list.
        /// </summary>
        /// <param name="m">The product to add. The message denotes how it is added.
        /// "Update" replaces at the specified index, "Add" adds it to the list.</param>
        public void ReceiveProduct(ProductMessage m)
        {
            Product product = new Product(m.ProductId, m.ProductName, m.ProductQuantity);
            if (m.Message == "Update")
            {
                _inventoryService.EditProduct(selectedProduct.ProductId, product);
                RaisePropertyChanged("ProductList");
            }
            else if (m.Message == "Add")
            {
                _inventoryService.SaveProduct(product);
                RaisePropertyChanged("ProductList");
            }
        }
        /// <summary>
        /// Gets text messages.
        /// </summary>
        /// <param name="msg">The received message. "Delete" means the currently selected product is deleted.</param>
        public void ReceiveMessage(NotificationMessage msg)
        {
            if (msg.Notification == "Delete")
            {
                _inventoryService.DeleteProduct(selectedProduct.ProductId);
                RaisePropertyChanged("ProductList");
            }
        }

        /// <summary>
        /// The list of registered members.
        /// </summary>
        public ObservableCollection<Product> ProductList
        {
            get { return _inventoryService.GetProducts(); }
        }
    }
}