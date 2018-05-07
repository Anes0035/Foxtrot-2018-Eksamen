using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FoxtrotProject.Model;

namespace FoxtrotProject.ViewModel
{
    class CustomerViewModel : ViewModel
    {
        List<Customer> customers;

        private Customer currentCustomer;

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

    }
}
