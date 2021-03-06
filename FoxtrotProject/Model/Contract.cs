﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxtrotProject.Model
{
    class Contract
    {
        //author Kasper and Christian
        public Customer Customer { get; set; }

        public int ID { get; set; }

        public DateTime StartDate { get; set; }

        public int Period { get; set; }

        public bool Status { get; set; }

        public List<ProductGroup> ProductGroups { get; set; }
        
        public Subscription Subscription { get; set; }

        public int Discount { get; set; }

        public Contract(bool createNewInstances)
        {
            if (createNewInstances)
            {
                Subscription = new Subscription();
                ProductGroups = new List<ProductGroup>();
            }
        }

        // Author Christian
        public Contract Clone()
        {
            return new Contract(false) { ID = ID, Customer = Customer, Period = Period, Status = Status, ProductGroups = ProductGroups, Subscription = Subscription, Discount = Discount, StartDate = StartDate};
        }
        // Author Christian
        public void AutoAssignId(ObservableCollection<Contract> contracts)
        {
            int counter = 0;
            do
            {
                counter++;
                ID = counter;
            }
            while (contracts.Where(p => p.ID == counter).Any());

        }

    }
}
