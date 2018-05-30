using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxtrotProject.Model
{
    class Contract
    {
        //author Kasper and Christian

        public int ID { get; set; }

        public DateTime StartDate { get; set; }

        public int Period { get; set; }

        public bool Status { get; set; }

        public Contract SelectedContract {get;set;}

        public List<ProductGroup> ProductGroups { get; set; }
        
        public Subscription Subscription { get; set; }

        public int Discount { get; set; }

        public Contract()
        {
            Subscription = new Subscription();
            ProductGroups = new List<ProductGroup>();
        }
        public Contract(int iD)
        {
            ID = iD;
        }

        public Contract Clone()
        {
            return new Contract() { ID = ID, Period = Period, Status = Status, Subscription = Subscription, Discount = Discount, StartDate = StartDate};
        }

        public Contract(List<Contract> contracts, string name, string description, double price, string category)
        {
            AutoAssignId(contracts);
            ID = ID;
            Period = Period;
            Status = Status;
            Subscription = Subscription;
            Discount = Discount;
            StartDate = StartDate;
        }

        private void AutoAssignId(List<Contract> contracts)
        {
            for (int i = 1; contracts.Where(p => p.ID == i).Any(); i++)
            {
                ID = i;
            }
        }

    }
}
