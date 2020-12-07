using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight; //For mvvmlight
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;//for class Messenger
using WPFMVVMMessenger.Messages;

namespace WPFMVVMMessenger.ViewModel
{
    public class PhSenderViewModel : ViewModelBase
    {
        private String _textBoxText;
        public RelayCommand OnClickCommand { get; set; }
        public string TextBoxText
        {
            get
            { return _textBoxText; }

            set
            {
                _textBoxText = value;
                RaisePropertyChanged("TextBoxText");
            }

        }

        public PhSenderViewModel()
        {
            OnClickCommand = new RelayCommand(OnClickCommandAction,
           null);
        }

        private void OnClickCommandAction()
        {
            var viewModelMessage = new PhViewModelMessage()
            {
                Text = TextBoxText
            };
            Messenger.Default.Send(viewModelMessage);
        }
    }
}
