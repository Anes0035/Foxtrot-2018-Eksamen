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

        public LogReader logReader { get; set; }

        public LogWriter logWriter { get; set; }

        public LogManager logManager { get; set; }

        public Database db { get; set; }

        private ObservableCollection<LogReader> logs;
        public ObservableCollection<LogReader> Logs
        {
            get { return logs; }
            set
            {
                logs = value;
                NotifyPropertyChanged();
            }
        }
        public DateTime Dt
        {
            get { return logReader.Dt ; }
            set
            {
                logReader.Dt = value;
                NotifyPropertyChanged();
            }
        }

        public string Message
        {
            get { return logReader.Message; }
            set
            {
                logReader.Message = value;
                NotifyPropertyChanged();
            }
        }

        // Author Kasper
        public LogViewModel()
        {
            logManager = new LogManager();

            db = new Database();
            logReader = new LogReader();
            logManager.logs = db.Logs();
            logs = new ObservableCollection<LogReader>(logManager.logs);

        }
   
    }
}
