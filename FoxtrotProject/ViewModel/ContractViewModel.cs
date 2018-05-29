using FoxtrotProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FoxtrotProject.ViewModel
{
    enum Activity
    {
        Aktiv,
        Inaktiv
    }

    class ContractViewModel : ViewModel, IDataErrorInfo
    {

        private ContractManager contractManager;

        #region Contract
        public ObservableCollection<Contract> Contracts { get; set; }

        private Contract contract;

        public int ID
        {
            get { return contract.ID; }
            set
            {
                contract.ID = value;
                NotifyPropertyChanged();
            }
        }
        private DateTime startDate;

        public DateTime StartDate
        {
            get { return contract.StartDate; }
            set
            {
                contract.StartDate = value;
                NotifyPropertyChanged();
            }
        }

        private string period;

        public string Period
        {
            get { return period; }
            set
            {
                period = value;
                NotifyPropertyChanged();
            }
        }

        public Activity status;

        public Activity Status
        {
            get { return status; }
            set
            {
                status = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<ProductGroup> productGroups;

        public ObservableCollection<ProductGroup> ProductGroups
        {
            get { return productGroups; }
            set { productGroups = value; }
        }


        public Subscription Subscription
        {
            get { return contract.Subscription; }
            set
            {
                contract.Subscription = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region ProductGroups
        public ObservableCollection<ProductGroup> AllProductGroups { get; set; }

        public ObservableCollection<string> ProductGroupNames
        {
            get { return GetProductGroupNames(); }
        }

        public ObservableCollection<string> GetProductGroupNames()
        {
            ObservableCollection<string> productGroupNames = new ObservableCollection<string>();

            foreach (ProductGroup pg in AllProductGroups)
            {
                productGroupNames.Add(pg.Name);
            }

            return productGroupNames;
        }
        #endregion

        private Contract selectedContract;

        public Contract SelectedContract
        {
            get { return selectedContract; }
            set
            {
                selectedContract = value;
                NotifyPropertyChanged();
            }
        }



        public ContractViewModel()
        {
            contract = new Contract();
            contractManager = new ContractManager();
            AllProductGroups = db.GetProductGroups();
            SaveContractCommand = new WpfCommand(SaveContractExecute, SaveContractCanExecute);
            RemoveContractCommand = new WpfCommand(RemoveContractExecute, RemoveContractCanExecute);
        }

        #region ErrorHandling
        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string columnName] => throw new NotImplementedException();
        #endregion

        #region SaveContractCommand
        public ICommand SaveContractCommand { get; set; }

        public void SaveContractExecute(object parameter)
        {
            Contracts.Add(contract.Clone());
            /* if(db.AddContract(contract))
             {
                 Contracts.Add(contract.Clone());
                 NotifyPropertyChanged("contracts");
                 db.LogAdd(7);
                 MessageBox.Show("Kontrakt Oprettet");
             }
             else
             {
                 MessageBox.Show("Fejl! Aftale eksisterer allerede!");
             }
            */
        }
        public bool SaveContractCanExecute(object paramter)
        {
            return true;
        }
        #endregion

        #region RemoveContractCommand
        public ICommand RemoveContractCommand { get; set; }

        public void RemoveContractExecute(object parameter)
        {
            /*
            ID = selectedContract.ID;
            Contracts.Remove(contract.Clone());
            db.RemoveContract(selectedContract);
            NotifyPropertyChanged("contracts");
            db.LogAdd(8);
            MessageBox.Show("Kontrakt Slettet");
            */
        }
        public bool RemoveContractCanExecute(object paramter)
        {
            return true;
        }
        #endregion

        #region UpdateContractCommand
        public ICommand UpdateContractCommand { get; set; }

        public void UpdateContractExecute(object parameter)
        {
            //db.LogAdd(9);
        }
        public bool UpdateContractCanExecute(object paramter)
        {
            return true;
        }
        #endregion
    }
}
