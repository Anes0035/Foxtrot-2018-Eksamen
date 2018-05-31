using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FoxtrotProject.Model;

namespace FoxtrotProject.ViewModel
{

    // Author Christian
    class StatisticViewModel : ViewModel
    {
        private Statistic statistic;

        private string x;

        public string X
        {
            get { return x; }
            set
            {
                x = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<string> topProductGroups;

        public ObservableCollection<string> TopProductGroups
        {
            get { return topProductGroups; }
            set
            {
                topProductGroups = value;
                NotifyPropertyChanged();
            }
        }

        public StatisticViewModel(ContractManager contractManager)
        {
            statistic = new Statistic(contractManager);
            ShowTopXCommand = new WpfCommand(ShowTopXExecute, ShowTopXCanExecute);
        }

        #region ErrorHandling
        public override string Error
        {
            get { return null; }
        }

        public override string this[string propertyName]
        {
            get
            {
                string message;
                switch (propertyName)
                {
                    case "X":
                        if (string.IsNullOrEmpty(X))
                            return PropertyIsEmptyErrorMessage(propertyName);

                        int x;
                        message = ValidateNumericParse<int>(X, propertyName, out x);

                        if (message != null)
                            return message;

                        statistic.X = x;
                        break;
                }
                return null;
            }
        }
        #endregion

        #region ShowTopXCommand
        public ICommand ShowTopXCommand { get; set; }

        public void ShowTopXExecute(object parameter)
        {
            TopProductGroups = statistic.FindTopXProducts();
        }

        public bool ShowTopXCanExecute(object parameter)
        {
            return true;
        }
        #endregion

    }
}
