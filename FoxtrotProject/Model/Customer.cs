﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxtrotProject.Model
{

    class Customer
    {
        // Author Kasper
        public string Name { get; set; }

        public string Address { get; set; }

        public int TelephoneNumber { get; set; }

        public string ContactPerson { get; set; }

        public double GrossIncome { get; set; }

        public int CVR { get; set; }

        public Customer SelectedCustomer { get; set; }
    
        
        //Author Kasper
        public Customer()
        {
            
        }
        public Customer Clone()
        {
            return new Customer() { CVR = CVR, Name = Name, Address = Address, ContactPerson = ContactPerson, GrossIncome = GrossIncome, TelephoneNumber = TelephoneNumber };
        }
        //Author Kasper
        public Customer(List<Customer> customers, int cvr, string name, string address, string contactPerson, double grossIncome, int telePhoneNumber)
        {
            cvr = CVR;
            name = Name;
            address = Address;
            contactPerson = ContactPerson;
            grossIncome = GrossIncome;
            telePhoneNumber = TelephoneNumber;
        }
        //Author Kasper
        public override string ToString()
        {
            return CVR + ", " + Name;
        }
    }
}
