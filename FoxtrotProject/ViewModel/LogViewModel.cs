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
    class LogViewModel : ViewModel, INotifyPropertyChanged
    {
        public ICommand ShowLogCommand { get; set; }

        public LogReader logReader { get; set;}

        public LogWriter logWriter { get; set; }

        public DateTime DT
        {
            get { return logReader.DT; }
            set {
                logReader.DT = value;
                NotifyPropertyChanged();
            }
        }

        public string Message
        {
            get { return logReader.Message; }
            set {
                logReader.Message = value;
                NotifyPropertyChanged();
            }
        }
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
        public LogViewModel()
        {

            db = new Database();
            logReader = new LogReader();
            Logs = db.Logs();

        }
        public void ShowLogExecute(object parameter)
        {
            Logs = db.Logs();
            MessageBox.Show("Log hentet!");
        }

        public bool ShowLogCanExecute(object parameter)
        {
            return true;
        }
    }
}
