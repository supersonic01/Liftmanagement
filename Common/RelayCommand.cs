using System;
using System.Windows.Input;

namespace Liftmanagement.Common
{
    public class RelayCommand : ICommand
    {
        readonly Action<object> Execute_;
        readonly Predicate<object> CanExecute_;

        public RelayCommand(Action<object> Execute, Predicate<object> CanExecute)
        {
            if (Execute == null)
                throw new ArgumentNullException("No action to execute for this command.");

            Execute_ = Execute;
            CanExecute_ = CanExecute;           
        }

        public bool CanExecute(object parameter)
        {
            return (CanExecute_ == null) ? true : CanExecute_(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            Execute_(parameter);
        }
    }
}