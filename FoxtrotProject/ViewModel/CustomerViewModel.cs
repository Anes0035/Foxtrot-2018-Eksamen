﻿using System;
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
    class CustomerViewModel : ViewModel
    {

        #region Customer
        private Customer customer;

        CustomerManager customerManager;

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
        private ObservableCollection<Customer> customers;
        public ObservableCollection<Customer> Customers
        {
            get { return customers; }
            set
            {
                customers = value;
                NotifyPropertyChanged();
            }
        }



        #endregion

        #region IDataErrorInfo

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

        //Author Anes,Kasper,Elena and Christian
        public CustomerViewModel()
        {
            customerManager = new CustomerManager();
            db = new Database();
            customer = new Customer();
            customerManager.customers = db.Customers();

            customers = new ObservableCollection<Customer>(customerManager.customers);
            SaveCustomerCommand = new WpfCommand(SaveCustomerExecute, SaveCustomerCanExecute);
            RemoveCustomerCommand = new WpfCommand(RemoveCustomerExecute, RemoveCustomerCanExecute);
            EditCustomerCommand = new WpfCommand(EditCustomerExecute, EditCustomerCanExecute);
            ClearCustomerCommand = new WpfCommand(ClearCustomerExecute, ClearCustomerCanExecute);

            SearchCustomerCommand = new WpfCommand(SearchCustomerExecute, SearchCustomerCanExecute);

        }


        #region SaveCustomerCommand
        public ICommand SaveCustomerCommand { get; set; }
        // Author Kasper
        public void SaveCustomerExecute(object parameter)
        {
            if (selectedcustomer == null)
            {
                if (db.AddCustomer(customer))
                {
                    Customers.Add(customer.Clone());
                    NotifyPropertyChanged("customers");
                    db.LogAdd(String.Format("Kunde med CVR: {0} blev oprettet", customer.CVR));
                    MessageBox.Show("Kunde Oprettet");
                    
                }
                else
                {
                    db.LogAdd(String.Format("Fejl! Kunde med CVR: {0} eksistere allerede", customer.CVR));
                    MessageBox.Show("Fejl! Kunde eksisterer allerede!");
                   
                }
            }
            else if (selectedcustomer != null)
            {
                db.EditCustomer(customer.Clone());
                Customers.Remove(selectedcustomer);
                Customers.Add(customer.Clone());
                NotifyPropertyChanged("customers");
                db.LogAdd(String.Format("Kunde med CVR: {0} blev redigeret", customer.CVR));
                MessageBox.Show("Kunde Rettet");
                customer.SelectedCustomer = null;
            }



        }

        // Author: Christian and Kasper Checking if every value is filled out correctly
        public bool SaveCustomerCanExecute(object parameter)
        {
            if (FirstErrorMessage != null)
                return false;
            else
                return true;
        }


        #endregion

        #region RemoveCustomerCommand
          public ICommand RemoveCustomerCommand { get; set; }


        // Author: Kasper  Removing customer from Collection and Database
        public void RemoveCustomerExecute(object parameter)
        {
            customer.CVR = selectedcustomer.CVR;
            db.RemoveCustomer(selectedcustomer);
            Customers.Remove(selectedcustomer);
            NotifyPropertyChanged("customers");
            db.LogAdd(String.Format("Kunde med CVR: {0} blev slettet", customer.CVR));
            MessageBox.Show("Kunde Slettet");
        }
    
   // Author Kasper
        public bool RemoveCustomerCanExecute(object parameter)
        {
            if (selectedcustomer == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        #endregion

        #region EditCustomerCommand

        public ICommand EditCustomerCommand { get; set; }       

        // Save customer to ObservableCollection and Database.

      //Author Kasper
        public void EditCustomerExecute(object parameter)
        {
        
            CVR = selectedcustomer.CVR.ToString();
            Name = selectedcustomer.Name;
            Address = selectedcustomer.Address;
            TelephoneNumber = selectedcustomer.TelephoneNumber.ToString();
            ContactPerson = selectedcustomer.ContactPerson;
            GrossIncome = selectedcustomer.GrossIncome.ToString();
        }
        // Author Kasper
        public bool EditCustomerCanExecute(object parameter)
        {
            if (selectedcustomer == null)
            {
                return false;
            }
            else
            {
                return true;
            }


        }

        #endregion

        #region  ClearCustomerCommand

        public ICommand ClearCustomerCommand { get; set; }
        // Author Kasper and Elena
        public void ClearCustomerExecute(object parameter)
        {
            CVR = "";
            Name = "";
            Address = "";
            TelephoneNumber = "";
            ContactPerson = "";
            GrossIncome = "";
        }
        // Author Kasper and Elena
        public bool ClearCustomerCanExecute(object parameter)
        {
            return true;

        }
        #endregion

        #region  SearchCustomerCommand
        // Author Elena
        public ICommand SearchCustomerCommand { get; set; }
        public string SearchCustomer { get; set; }
        // Author Elena
        public void SearchCustomerExecute(object parameter)
        {
            try
            {               

                customers = new ObservableCollection<Customer>(customerManager.customers);
               
                foreach (Customer c in customers.ToList())
                {
                    

                    if (!c.CVR.ToString().ToLower().StartsWith(SearchCustomer.ToLower()) &&
                        !c.Name.ToString().ToLower().StartsWith(SearchCustomer.ToLower()) &&
                        !c.Address.ToString().ToLower().StartsWith(SearchCustomer.ToLower())                        )
                    {
                        customers.Remove(c);
                    }
                }


                    NotifyPropertyChanged("customers");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }



        }
        // Author Elena
        public bool SearchCustomerCanExecute(object parameter)
        {
            return true;
        }

        #endregion
    }
}
