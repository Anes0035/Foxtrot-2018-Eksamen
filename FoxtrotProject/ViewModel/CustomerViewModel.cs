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
                    case "CVR":
                        if (String.IsNullOrEmpty(CVR))
                            return PropertyIsEmptyErrorMessage(propertyName);

                        int cVR;
                        message = ValidateIntegerParse(CVR, propertyName, out cVR);

                        if (message != null)
                            return message;

                        customer.CVR = cVR;
                        break;
                    case "Name":
                        if (String.IsNullOrEmpty(Name))
                            return PropertyIsEmptyErrorMessage("Navn");
                        break;
                    case "Address":
                        if (String.IsNullOrEmpty(Address))
                            return PropertyIsEmptyErrorMessage("Adresse");
                        break;
                    case "TelephoneNumber":
                        if (String.IsNullOrEmpty(TelephoneNumber))
                            return PropertyIsEmptyErrorMessage("Tlf. Nummer");

                        int telephoneNumber;
                        message = ValidateIntegerParse(TelephoneNumber, propertyName, out telephoneNumber);

                        if (message != null)
                            return message;

                        customer.TelephoneNumber = telephoneNumber;
                        break;
                    case "ContactPerson":
                        if (String.IsNullOrEmpty(ContactPerson))
                            return PropertyIsEmptyErrorMessage("Kontakt Person");
                        break;
                    case "GrossIncome":
                        if (String.IsNullOrEmpty(GrossIncome))
                            return PropertyIsEmptyErrorMessage("Årlig Omsætning");

                        double grossIncome;
                        message = ValidateDoubleParse(GrossIncome, "Årlig Omsætning", out grossIncome);

                        if (message != null)
                            return message;

                        customer.GrossIncome = grossIncome;
                        break;
                }

                return null;
            }
        }
        #endregion

        private Database db;

        public CustomerViewModel()
        {
            db = new Database();
            customer = new Customer();
            Customers = db.Customers();
            SaveCustomerCommand = new WpfCommand(SaveCustomerExecute, SaveCustomerCanExecute);
            RemoveCustomerCommand = new WpfCommand(RemoveCustomerExecute, RemoveCustomerCanExecute);
            
        }

        #region SaveCustomerCommand
        public ICommand SaveCustomerCommand { get; set; }

        public ICommand RemoveCustomerCommand { get; set; }
        public void SaveCustomerExecute(object parameter)
        {
            Customers.Add(customer.Clone());
            db.AddCustomer(customer.Clone());
            NotifyPropertyChanged("customers");
            MessageBox.Show("Kunde Oprettet");

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
          
            Customers.Remove(customer.Clone());
            db.RemoveCustomer(customer.Clone());
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
