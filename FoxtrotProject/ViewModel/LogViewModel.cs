using FoxtrotProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FoxtrotProject.ViewModel
{
    // Author Kasper
    class LogViewModel : ViewModel, INotifyPropertyChanged
    {
        public ICommand ShowLogCommand { get; set; }

        public LogManager logManager { get; set; }

        private ObservableCollection<DataEntry> logs;
        public ObservableCollection<DataEntry> Logs
        {
            get { return logs; }
            set
            {
                logs = value;
                NotifyPropertyChanged();
            }
        }

        // Author Kasper and Christian
        public LogViewModel()
        {
            logManager = new LogManager();

            logManager.logs = db.Logs();
            logs = new ObservableCollection<DataEntry>(logManager.logs);
            UpdateLogCommand = new WpfCommand(UpdateLogExecute, UpdateLogCanExecute);

        }

        #region UpdateLogCommand
        public ICommand UpdateLogCommand { get; set; }
        // Author Kasper and Christian
        public void UpdateLogExecute(object parameter)
        {
            logManager.logs = db.Logs();
            Logs = new ObservableCollection<DataEntry>(logManager.logs);
        }
        // Author Kasper and Christian
        public bool UpdateLogCanExecute(object parameter)
        {
            return true;
        }
        #endregion

    }
}
