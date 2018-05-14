using FoxtrotProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxtrotProject.ViewModel
{
    class ContractViewModel : ViewModel
    {
        private Contract currentContract;

        private ContractManager contractManager;

        private Dictionary<ProductGroup, int> ProductGroupCount;

        private DateTime startDate;

        public DateTime StartDate
        {
            get { return currentContract.StartDate; }
            set
            {
                currentContract.StartDate = value;
                NotifyPropertyChanged();
            }
        }

        private int period;

        public int Period
        {
            get { return currentContract.Period; }
            set
            {
                currentContract.Period = value;
                NotifyPropertyChanged();
            }
        }

        private bool status;

        public bool Status
        {
            get { return currentContract.Status; }
            set
            {
                currentContract.Status = value;
                NotifyPropertyChanged();
            }
        }

        private Subscription subscription;

        public Subscription Subscription
        {
            get { return currentContract.Subscription; }
            set
            {
                currentContract.Subscription = value;
                NotifyPropertyChanged();
            }
        }

        public ContractViewModel()
        {
            ProductGroupCount = new Dictionary<ProductGroup, int>();
            contractManager.onAddContract += new EventHandler<ContractEventArgs>(CountProductGroup);
        }

        private void CountProductGroup(object sender, ContractEventArgs e)
        {
            foreach (ProductGroup pg in e.contract.ProductGroups)
            {
                if (!ProductGroupCount.ContainsKey(pg))
                    ProductGroupCount.Add(pg, 1);
                else
                    ProductGroupCount[pg]++;
            }
        }
    }
}
