using FoxtrotProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FoxtrotProject.ViewModel
{
    class ContractViewModel : ViewModel
    {
        public Contract contract { get; set; }
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
        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
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

        private string status;

        public string Status
        {
            get { return currentContract.Status; }
            set
            {
                currentContract.Status = value;
                NotifyPropertyChanged();
            }
        }

        private int discount;

        public int Discount
        {
            get { return discount; }
            set { discount = value; }
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

        public ObservableCollection<Contract> Contracts { get; set; }

        private Contract selectedcontract;

        public Contract SelectedContract
        {
            get { return selectedcontract; }
            set
            {
                selectedcontract = value;
                NotifyPropertyChanged();
            }
        }

        public ContractViewModel()
        {
            contract = new Contract();
            ProductGroupCount = new Dictionary<ProductGroup, int>();
            Contracts = db.Contracts();
            contractManager.onAddContract += new EventHandler<ContractEventArgs>(CountProductGroup);
            SaveContractCommand = new WpfCommand(SaveContractExecute, SaveContractCanExecute);
            RemoveContractCommand = new WpfCommand(RemoveContractExecute, RemoveContractCanExecute);
        }
        public ICommand SaveContractCommand { get; set; }
        public ICommand RemoveContractCommand { get; set; }
        public ICommand UpdateContractCommand { get; set; }


        private void CountProductGroup(object sender, ContractEventArgs e)
        {
            foreach (ProductGroup pg in e.contract.ContractGroups)
            {
                if (!ProductGroupCount.ContainsKey(pg))
                    ProductGroupCount.Add(pg, 1);
                else
                    ProductGroupCount[pg]++;
            }
        }

     
        public void SaveContractExecute(object parameter)
        {
            if(db.ContractExist(contract))
            {
                Contracts.Add(contract.Clone());
                db.AddContract(contract.Clone());
                NotifyPropertyChanged("contracts");
                MessageBox.Show("Kontrakt Oprettet");
            }
            else
            {
                MessageBox.Show("Fejl! Aftale eksisterer allerede!");
            }
           
        }
        public bool SaveContractCanExecute(object paramter)
        {
            return true;
        }
        public void RemoveContractExecute(object parameter)
        {
           
            ID = selectedcontract.ID;
            Contracts.Remove(contract.Clone());
            db.RemoveContract(selectedcontract);
            NotifyPropertyChanged("contracts");
            MessageBox.Show("Kontrakt Slettet");
        }
        public bool RemoveContractCanExecute(object paramter)
        {
            return true;
        }

        public void UpdateContractExecute(object parameter)
        {

        }
        public bool UpdateContractCanExecute(object paramter)
        {
            return true;
        }
    }
}
