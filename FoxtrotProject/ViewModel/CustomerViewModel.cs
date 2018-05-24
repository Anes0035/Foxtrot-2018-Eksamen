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
using System.Reflection;

namespace FoxtrotProject.ViewModel
{
    class CustomerViewModel : ViewModel, IDataErrorInfo, INotifyPropertyChanged
    {

        #region Customer
        public Customer customer { get; set; }

        public string Name
        {
            get { return customer.Name; }
            set
            {
                customer.Name = value;
                NotifyPropertyChanged();
            }
        }

        public string Address
        {
            get { return customer.Address; }
            set
            {
                customer.Address = value;
                NotifyPropertyChanged();
            }
        }

        private string telephoneNumber;

        public string TelephoneNumber
        {
            get { return telephoneNumber; }
            set
            {
                telephoneNumber = value;
                NotifyPropertyChanged();
            }
        }

        public string ContactPerson
        {
            get { return customer.ContactPerson; }
            set
            {
                customer.ContactPerson = value;
                NotifyPropertyChanged();
            }
        }

        private string grossIncome;

        public string GrossIncome
        {
            get { return grossIncome; }
            set
            {
                grossIncome = value;
                NotifyPropertyChanged();
            }
        }

        private string cVR;

        public string CVR
        {
            get { return cVR; }
            set
            {
                cVR = value;
                NotifyPropertyChanged();
            }
        }
        private Customer selectedcustomer;

        public Customer SelectedCustomer
        {
            get { return selectedcustomer; }
            set
            {
                selectedcustomer = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<Customer> Customers { get; set; }

        

        #endregion

        #region IDataErrorInfo
        public string FirstErrorMessage
        {
            get
            {
                PropertyInfo[] properties = GetType().GetProperties();
                foreach (PropertyInfo p in properties)
                {
                    if (this[p.Name] != null)
                        return this[p.Name];
                }

                return null;
            }
        }

        public string Error
        {
            get { return null; }
        }

        public string this[string propertyName]
        {
            get
            {
                string message;
                switch (propertyName)
                {
                    case "ContactPerson":
                    case "Address":
                    case "Name":
                        if (String.IsNullOrEmpty((string)GetType().GetProperty(propertyName).GetValue(this)))
                            return PropertyIsEmptyErrorMessage(propertyName);
                        break;
                    case "CVR":
                        if (String.IsNullOrEmpty(CVR))
                            return PropertyIsEmptyErrorMessage(propertyName);

                        int cVR;
                        message = ValidateNumericParse<int>(CVR, propertyName, out cVR);

                        if (message != null)
                            return message;

                        customer.CVR = cVR;
                        break;
                    case "TelephoneNumber":
                        if (String.IsNullOrEmpty(TelephoneNumber))
                            return PropertyIsEmptyErrorMessage(propertyName);

                        int telephoneNumber;
                        message = ValidateNumericParse<int>(TelephoneNumber, propertyName, out telephoneNumber);

                        if (message != null)
                            return message;

                        customer.TelephoneNumber = telephoneNumber;
                        break;
                    case "GrossIncome":
                        if (String.IsNullOrEmpty(GrossIncome))
                            return PropertyIsEmptyErrorMessage(propertyName);

                        double grossIncome;
                        message = ValidateNumericParse<double>(GrossIncome, propertyName, out grossIncome);

                        if (message != null)
                            return message;

                        customer.GrossIncome = grossIncome;
                        break;
                }

                return null;
            }
        }
        #endregion

        public CustomerViewModel()
        {
            db = new Database();
            customer = new Customer();
            Customers = db.Customers();
            SaveCustomerCommand = new WpfCommand(SaveCustomerExecute, SaveCustomerCanExecute);
            RemoveCustomerCommand = new WpfCommand(RemoveCustomerExecute, RemoveCustomerCanExecute);
            EditCustomerCommand = new WpfCommand(EditCustomerExecute, EditCustomerCanExecute);
        }

        #region SaveCustomerCommand
        public ICommand SaveCustomerCommand { get; set; }

        public ICommand RemoveCustomerCommand { get; set; }

        public ICommand EditCustomerCommand { get; set; }
        public void SaveCustomerExecute(object parameter)
        {

            if (db.CustomerExist(customer))
            {
                Customers.Add(customer.Clone());
                db.AddCustomer(customer.Clone());
                NotifyPropertyChanged("customers");
                MessageBox.Show("Kunde Oprettet");
            }
            else
            {
                MessageBox.Show("Fejl! Kunde eksisterer allerede!");
            }
                
            

        }

        public bool SaveCustomerCanExecute(object parameter)
        {
            if (FirstErrorMessage != null)
                return false;
            else
                return true;
        }

        public void RemoveCustomerExecute(object parameter)
        {
            CVR = selectedcustomer.CVR.ToString();
            Customers.Remove(customer.Clone());
            db.RemoveCustomer(selectedcustomer);
            NotifyPropertyChanged("customers");
            MessageBox.Show("Kunde Slettet");

        }

        public bool RemoveCustomerCanExecute(object parameter)
        {
                return true;
        }

        public void EditCustomerExecute(object parameter)
        {
           
        }

        public bool EditCustomerCanExecute(object parameter)
        {
            
            return true;
        }
        #endregion

    }
}
