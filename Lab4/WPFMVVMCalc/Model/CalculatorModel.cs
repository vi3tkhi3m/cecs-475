using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFMVVMCalc.Model
{
    public class CalculatorModel
    {
        public string FirstOperand { get; set; }
        public string SecondOperand { get; set; }
        public string Operation { get; set; }
        public string Result { get; set; }

        public CalculatorModel()
        {
            FirstOperand = string.Empty;
            SecondOperand = string.Empty;
            Operation = string.Empty;
            Result = string.Empty;
        }

        public void Calculate()
        {
            switch (Operation)
            {
                case ("+"):
                    Result = (Convert.ToDouble(FirstOperand) + Convert.ToDouble(SecondOperand)).ToString();
                    break;

                case ("-"):
                    Result = (Convert.ToDouble(FirstOperand) - Convert.ToDouble(SecondOperand)).ToString();
                    break;

                case ("*"):
                    Result = (Convert.ToDouble(FirstOperand) * Convert.ToDouble(SecondOperand)).ToString();
                    break;

                case ("/"):
                    Result = (Convert.ToDouble(FirstOperand) / Convert.ToDouble(SecondOperand)).ToString();
                    break;
            }
        }
    }
}
