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
    //Author Christian
    enum Activity
    {
        Aktiv,
        Inaktiv
    }
    //Author Christian and Kasper
    class ContractViewModel : ViewModel
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
            get { return startDate; }
            set
            {
                startDate = value;
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

        public bool Status
        {
            get { return contract.Status; }
            set
            {
                contract.Status = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<ProductGroup> productGroups;

        public ObservableCollection<ProductGroup> ProductGroups
        {
            get { return productGroups; }
            set
            {
                productGroups = value;
                NotifyPropertyChanged();
            }
        }


        public bool Subscription
        {
            get { return contract.Subscription.Status; }
            set
            {
                contract.Subscription.Status = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region ProductGroups

        private ObservableCollection<ProductGroup> AllProductGroups;

        public ObservableCollection<ProductGroup> ShownProductGroups { get; set; }

        private ProductGroup dtgSelectedProductGroup;

        public ProductGroup DtgSelectedProductGroup
        {
            get { return dtgSelectedProductGroup; }
            set
            {
                dtgSelectedProductGroup = value;
                NotifyPropertyChanged();
            }
        }

        private ProductGroup cbxSelectedProductGroup;

        public ProductGroup CbxSelectedProductGroup
        {
            get { return cbxSelectedProductGroup; }
            set
            {
                cbxSelectedProductGroup = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        private Contract selectedContract;
        public string message;
        public Contract SelectedContract
        {
            get { return selectedContract; }
            set
            {
                selectedContract = value;
                NotifyPropertyChanged();
            }
        }


        //Author Christian and Kasper
        public ContractViewModel()
        {
            contract = new Contract();
            productGroups = new ObservableCollection<ProductGroup>();
            StartDate = DateTime.Now;

            Contracts = new ObservableCollection<Contract>();

            contractManager = new ContractManager();
            AllProductGroups = db.GetProductGroups();
            ShownProductGroups = AllProductGroups;
            AddProductGroupCommand = new WpfCommand(AddProductGroupExecute, AddProductGroupCanExecute);
            RemoveProductGroupCommand = new WpfCommand(RemoveProductGroupExecute, RemoveProductGroupCanExecute);
            ClearContractCommand = new WpfCommand(ClearContractExecute, ClearContractCanExecute);
            SaveContractCommand = new WpfCommand(SaveContractExecute, SaveContractCanExecute);
            RemoveContractCommand = new WpfCommand(RemoveContractExecute, RemoveContractCanExecute);
        }

        #region ErrorHandling
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
                    case "StartDate":
                        if (StartDate < DateTime.Today)
                            return "Kontrakten kan ikke starte i fortiden";

                        contract.StartDate = StartDate;
                        break;

                    case "Period":
                        if (string.IsNullOrEmpty((string)GetType().GetProperty(propertyName).GetValue(this)))
                            return PropertyIsEmptyErrorMessage(propertyName);

                        int period;
                        message = ValidateNumericParse<int>(Period, propertyName, out period);

                        if (message != null)
                            return message;

                        if (period < 6 || period > 24)
                            return "Perioden skal være mellem 6 og 24 måneder";

                        contract.Period = period;
                        break;

                    case "ProductGroups":
                        if (!ProductGroups.Any())
                            return "Der er ikke valgt nogle produkt grupper";

                        contract.ProductGroups = new List<ProductGroup>(ProductGroups);
                        break;
                }
                return null;
            }
        }
        #endregion

        #region AddProductGroupCommand
        public ICommand AddProductGroupCommand { get; set; }
        //Author Christian
        public void AddProductGroupExecute(object parameter)
        {
            ProductGroups.Add(CbxSelectedProductGroup);
            ShownProductGroups.Remove(CbxSelectedProductGroup);
            NotifyPropertyChanged("ShownProductGroups");
            NotifyPropertyChanged("ProductGroups");
        }
        //Author Christian 
        public bool AddProductGroupCanExecute(object parameter)
        {
            if (cbxSelectedProductGroup != null)
                return true;
            else
                return false;
        }
        #endregion

        #region RemoveProductGroupCommand

        public ICommand RemoveProductGroupCommand { get; set; }
        //Author Christian
        public void RemoveProductGroupExecute(object parameter)
        {
            ShownProductGroups.Add(DtgSelectedProductGroup);
            ProductGroups.Remove(DtgSelectedProductGroup);
            NotifyPropertyChanged("ShownProductGroups");
            NotifyPropertyChanged("ProductGroups");
        }

        public bool RemoveProductGroupCanExecute(object parameter)
        {
            if (dtgSelectedProductGroup != null)
                return true;
            else
                return false;
        }

        #endregion

        #region ClearContractCommand
        //Author Christian
        public ICommand ClearContractCommand { get; set; }
        //Author Christian 
        public void ClearContractExecute(object parameter)
        {
            ID = 0;
            StartDate = DateTime.Today;
            Status = false;
            Period = null;
            Subscription = false;
            ProductGroups = new ObservableCollection<ProductGroup>();
        }

        public bool ClearContractCanExecute(object parameter)
        {
            return true;
        }
        #endregion

        #region SaveContractCommand
        public ICommand SaveContractCommand { get; set; }
        //Author Christian and Kasper
        public void SaveContractExecute(object parameter)
        {
            Contracts.Add(contract.Clone());
            NotifyPropertyChanged("Contracts");
            /*Contracts.Add(contract.Clone());
            if (selectedContract == null)
            {
                if (db.AddContract(contract))
                {
                    message = "Aftale oprettet";
                    db.AddContract(contract.Clone());
                    Contracts.Add(contract.Clone());
                    NotifyPropertyChanged("Contracts");
                    db.LogAdd(message);
                    MessageBox.Show("Aftale Oprettet");
                }
                else
                {
                    MessageBox.Show("Fejl! Aftale eksisterer allerede!");
                }

            }
            else if (selectedContract != null)
            {
                message = "Aftale redigeret";
                db.UpdateContract(contract.Clone());
                Contracts.Remove(selectedContract);
                Contracts.Add(contract.Clone());
                NotifyPropertyChanged("Contracts");
                db.LogAdd(message);
                MessageBox.Show("Aftale redigeret");
            }
            */
        }
        //Author Christian and Kasper
        public bool SaveContractCanExecute(object paramter)
        {
            if (FirstErrorMessage != null)
                return false;
            else
                return true;
        }
        #endregion

        #region RemoveContractCommand
        public ICommand RemoveContractCommand { get; set; }
        //Author Kasper
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
        //Author Kasper and Anes
        public bool RemoveContractCanExecute(object paramter)
        {
            return true;
        }
        #endregion

        #region UpdateContractCommand
        public ICommand UpdateContractCommand { get; set; }

        // Author Kasper and Anes
        public void UpdateContractExecute(object parameter)
        {
            contract.ID = selectedContract.ID;
            contract.Period = selectedContract.Period;
            contract.StartDate = selectedContract.StartDate;
            contract.Status = selectedContract.Status;
            contract.Subscription = selectedContract.Subscription;
            contract.ProductGroups = selectedContract.ProductGroups;
            contract.Discount = selectedContract.Discount;
        }
        //Author Anes and Kasper
        public bool UpdateContractCanExecute(object paramter)
        {
            return true;
        }
        #endregion
    }
}
