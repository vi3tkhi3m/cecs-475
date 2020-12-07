using GalaSoft.MvvmLight.Messaging;


namespace WPFMVVMMessenger.Messages
{
    class PhViewModelMessage : MessageBase
    {
        public string Text { get; set; }
    }

}
