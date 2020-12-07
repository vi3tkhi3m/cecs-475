using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using InventoryApp.Message;
using InventoryApp.Model;
using InventoryApp.Repository;
using InventoryApp.Service;
using InventoryApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace InventoryApp.ViewModel
{
    public class AddViewModel : ViewModelBase
    {
        private readonly IInventoryService _inventoryService;

        /// <summary>
        /// The currently entered product id in the add window.
        /// </summary>
        private int enteredProductId;
        /// <summary>
        /// The currently entered product name in the add window.
        /// </summary>
        private string enteredProductName;
        /// <summary>
        /// The currently entered product quantity in the add window.
        /// </summary>
        private int enteredProductQuantity;

        /// <summary>
        /// The command that triggers saving the filled out product data.
        /// </summary>
        public ICommand SaveCommand { get; private set; }
        /// <summary>
        /// The command that triggers closing the add window.
        /// </summary>
        public ICommand CancelCommand { get; private set; }

        public AddViewModel(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
            SaveCommand = new RelayCommand<IClosable>(SaveMethod);
            CancelCommand = new RelayCommand<IClosable>(CancelMethod);
        }

        /// <summary>
        /// Sends a valid product to the Main VM to add to the list, then closes the window.
        /// </summary>
        /// <param name="window">The window to close.</param>
        public void SaveMethod(IClosable window)
        {
            try
            {
                Product product = new Product(EnteredProductId, EnteredProductName, EnteredProductQuantity);
                if (window != null && _inventoryService.IsProductValid(product))
                {
                    var productMessage = new ProductMessage(product, "Add");     
                    Messenger.Default.Send(productMessage);
                    window.Close();
                    clearFields();
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
        /// Closes the window.
        /// </summary>
        /// <param name="window">The window to close.</param>
        public void CancelMethod(IClosable window)
        {
            if (window != null)
            {
                window.Close();
            }
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

        private void clearFields()
        {
            enteredProductId = 0;
            enteredProductName = "";
            enteredProductQuantity = 0;
        }

    }
}
