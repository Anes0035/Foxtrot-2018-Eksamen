using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FoxtrotProject.ViewModel
{
    class WpfCommand : ICommand
    {
        private Action<object> execute;

        private Predicate<object> canExecute;
        //Author Christian and Anes
        public WpfCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        //Author Christian and Anes
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        //Author Christian and Anes
        public bool CanExecute(object parameter)
        {
            return canExecute(parameter);
        }
        //Author Christian and Anes
        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
