﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFMVVMCalc.Command
{
    public class CalculatorCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action<string> _execute;

        public CalculatorCommand(Action<string> execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute.Invoke(parameter as string);
        }
    }
}
