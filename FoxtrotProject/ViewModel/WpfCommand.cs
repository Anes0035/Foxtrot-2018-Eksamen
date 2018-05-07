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

        public WpfCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return CanExecute(parameter);
        }

        public void Execute(object parameter)
        {
            Execute(parameter);
        }
    }
}
