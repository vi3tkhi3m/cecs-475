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
    public class PhReceiverViewModel : ViewModelBase
    {
        private string _contentText;
        public string ContentText
        {
            get { return _contentText; }
            set
            {
                _contentText = value;
                RaisePropertyChanged("ContentText");
            }
        }

        public PhReceiverViewModel()
        {
            Messenger.Default.Register<PhViewModelMessage>(this,
           OnReceiveMessageAction);
        }

        private void OnReceiveMessageAction(PhViewModelMessage obj)
        {
            ContentText = obj.Text;
        }
    }
}
