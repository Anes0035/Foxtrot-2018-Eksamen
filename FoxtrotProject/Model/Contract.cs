using System;
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

        public int ID { get; set; }

        public DateTime StartDate { get; set; }

        public int Period { get; set; }

        public bool Status { get; set; }

        public List<ProductGroup> ProductGroups { get; set; }
        
        public Subscription Subscription { get; set; }

        public int Discount { get; set; }

        public Contract()
        {
            Subscription = new Subscription();
            ProductGroups = new List<ProductGroup>();
        }

        public Contract Clone()
        {
            return new Contract() { ID = ID, Period = Period, Status = Status, ProductGroups = ProductGroups, Subscription = Subscription, Discount = Discount, StartDate = StartDate};
        }

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
