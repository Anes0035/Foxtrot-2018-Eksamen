using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FoxtrotProject.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;

namespace FoxtrotProject.ViewModel
{
    class CustomerViewModel : ViewModel
    {
        public ObservableCollection<Customer> Customers { get; set; }

        private Customer currentCustomer;

        public ICommand SaveCustomerCommand { get; set; }

        public string Name
        {
            get { return currentCustomer.Name; }
            set
            {
                currentCustomer.Name = value;
                NotifyPropertyChanged();
            }
        }

        public string Address
        {
            get { return currentCustomer.Address; }
            set
            {
                currentCustomer.Address = value;
                NotifyPropertyChanged();
            }
        }

        public int TelephoneNumber
        {
            get { return currentCustomer.TelephoneNumber; }
            set
            {
                currentCustomer.TelephoneNumber = value;
                NotifyPropertyChanged();
            }
        }

        public string ContactPerson
        {
            get { return currentCustomer.ContactPerson; }
            set
            {
                currentCustomer.ContactPerson = value;
                NotifyPropertyChanged();
            }
        }

        public double GrossIncome
        {
            get { return currentCustomer.GrossIncome; }
            set
            {
                currentCustomer.GrossIncome = value;
                NotifyPropertyChanged();
            }
        }

        public int CVR
        {
            get { return currentCustomer.CVR; }
            set
            {
                currentCustomer.CVR = value;
                NotifyPropertyChanged();
            }
        }

        private string customerErrorMessage;

        public string CustomerErrorMessage
        {
            get { return customerErrorMessage; }
            set
            {
                customerErrorMessage = value;
                NotifyPropertyChanged();
            }
        }

        public CustomerViewModel()
        {
            currentCustomer = new Customer();
            Customers = new ObservableCollection<Customer>();
            SaveCustomerCommand = new WpfCommand(SaveCustomerExecute, SaveCustomerCanExecute);
        }

        public void SaveCustomerExecute(object parameter)
        {
            Customers.Add(currentCustomer);
            NotifyPropertyChanged("customers");
            MessageBox.Show("Kunde Oprettet");
            
        }

        public bool SaveCustomerCanExecute(object parameter)
        {
            return true;
        }

    }
}
