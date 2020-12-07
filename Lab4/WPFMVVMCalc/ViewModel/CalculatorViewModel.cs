using System.ComponentModel;
using WPFMVVMCalc.Command;
using WPFMVVMCalc.Model;

namespace WPFMVVMCalc.ViewModel
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        private CalculatorModel calculation;

        private string calculationLog = "";
        private string display = "";

        public event PropertyChangedEventHandler PropertyChanged;

        public CalculatorCommand DigitButtonPressCommand { get; private set; }
        public CalculatorCommand OperationButtonPressCommand { get; private set; }


        public CalculatorViewModel()
        {
            calculation = new CalculatorModel();
            DigitButtonPressCommand = new CalculatorCommand(HandleDigit);
            OperationButtonPressCommand = new CalculatorCommand(HandleOperation);
        }

        private void HandleDigit(string digit)
        {
            Display += digit;
        }

        private void HandleOperation(string operation)
        {
            switch (operation)
            {
                case "C":
                    Display = "";
                    CalculationLog = "";
                    calculation.FirstOperand = string.Empty;
                    calculation.SecondOperand = string.Empty;
                    calculation.Operation = string.Empty;
                    calculation.Result = string.Empty;
                    break;
                case "Del":
                    if (display.Length > 1)
                        Display = display.Substring(0, display.Length - 1);
                    else Display = "";
                    break;
                case "+":
                case "-":
                case "/":
                case "*":
                    if (calculation.FirstOperand == null || calculation.FirstOperand == "")
                    {
                        calculation.FirstOperand = Display;
                        CalculationLog += Display;
                        calculation.Operation = operation;
                        CalculationLog += " " + operation;
                        Display = "";
                    }
                    break;
                case "=":
                    if (calculation.FirstOperand != null || calculation.FirstOperand != "")
                    {
                        calculation.SecondOperand = Display;
                        CalculationLog += " " + Display;
                        calculation.Calculate();
                        if (calculation.Result != null || calculation.Result != "")
                        {
                            Display = Result;
                        }
                    }
                    break;
            }
        }

        public string Display
        {
            get { return display; }
            set
            {
                display = value;
                NotifyPropertyChanged("Display");
            }
        }

        public string CalculationLog
        {
            get { return calculationLog; }
            set
            {
                calculationLog = value;
                NotifyPropertyChanged("CalculationLog");
            }
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            if(propertyName != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string Result
        {
            get { return calculation.Result; }
        }
    }
}
