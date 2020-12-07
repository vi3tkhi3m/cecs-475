using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using InventoryApp.Message;
using InventoryApp.Model;
using InventoryApp.Service;
using InventoryApp.Util;
using System;
using System.Windows;
using System.Windows.Input;

namespace InventoryApp.ViewModel
{
    public class EditViewModel : ViewModelBase
    {
        private readonly IInventoryService _inventoryService;

        private int enteredProductId;
        private string enteredProductName;
        private int enteredProductQuantity;

        /// <summary>
        /// The command that triggers saving the filled out member data.
        /// </summary>
        public ICommand UpdateCommand { get; private set; }
        /// <summary>
        /// The command that triggers removing the previously selected user.
        /// </summary>
        public ICommand DeleteCommand { get; private set; }

        private int productIdBeforeEdit;

        public EditViewModel(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
            UpdateCommand = new RelayCommand<IClosable>(UpdateMethod);
            DeleteCommand = new RelayCommand<IClosable>(DeleteMethod);
            Messenger.Default.Register<Product>(this, GetSelected);
        }
        
        /// <summary>
        /// Sends a valid member to the main VM to replace at the selected index with, then closes the change window.
        /// </summary>
        /// <param name="window">The window to close.</param>
        public void UpdateMethod(IClosable window)
        {
            try
            {
                Product product = new Product(EnteredProductId, EnteredProductName, EnteredProductQuantity);
                if (window != null && _inventoryService.IsProductValid(productIdBeforeEdit, product))
                {
                    var productMessage = new ProductMessage(product, "Update");
                    Messenger.Default.Send(productMessage);
                    window.Close();
                }
            }
            catch (ArgumentException e)
            {
                MessageBox.Show(e.Message, "Entry Error");
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Fields cannot be empty.", "Entry Error");
            }
            catch (FormatException)
            {
                MessageBox.Show($"Quantity must be {Globals.PRODUCT_MIN_QUANTITY}-{Globals.PRODUCT_MAX_QUANTITY}.", "Entry Error");
            }
        }

        /// <summary>
        /// Sends out a message to initiate closing the change window.
        /// </summary>
        /// <param name="window">The window to close.</param>
        public void DeleteMethod(IClosable window)
        {
            if (window != null)
            {
                Messenger.Default.Send(new NotificationMessage("Delete"));
                window.Close();
            }
        }

        /// <summary>
        /// Receives a product from the main VM to auto-fill the change box with the currently selected product.
        /// </summary>
        /// <param name="m">The product data to fill in.</param>
        public void GetSelected(Product m)
        {
            productIdBeforeEdit = m.ProductId;
            EnteredProductId = m.ProductId;
            EnteredProductName = m.ProductName;
            EnteredProductQuantity = m.ProductQuantity;
        }

        /// <summary>
        /// The currently entered product id in the add window.
        /// </summary>
        public int EnteredProductId
        {
            get
            {
                return enteredProductId;
            }
            set
            {
                enteredProductId = value;
                RaisePropertyChanged("EnteredProductId");
            }
        }

        /// <summary>
        /// The currently entered product name in the add window.
        /// </summary>
        public string EnteredProductName
        {
            get
            {
                return enteredProductName;
            }
            set
            {
                enteredProductName = value;
                RaisePropertyChanged("EnteredProductName");
            }
        }

        /// <summary>
        /// The currently entered product quantity in the add window.
        /// </summary>
        public int EnteredProductQuantity
        {
            get
            {
                return enteredProductQuantity;
            }
            set
            {
                enteredProductQuantity = value;
                RaisePropertyChanged("EnteredProductQuantity");
            }
        }
    }
}
