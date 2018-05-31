﻿using FoxtrotProject.Model;
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

    class ContractViewModel : ViewModel
    {

        public ContractManager ContractManager { get; set; }

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

        public Customer Customer
        {
            get { return contract.Customer; }
            set
            {
                contract.Customer = value;
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
            set { SetStatus(); }
        }

        public void SetStatus()
        {
            if (StartDate < DateTime.Today|| StartDate.AddMonths(contract.Period) < DateTime.Today)
                contract.Status = false;
            else
                contract.Status = true;
            NotifyPropertyChanged("Status");
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

        private string discount;

        public string Discount
        {
            get { return discount; }
            set
            {
                discount = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region ProductGroups

        private ObservableCollection<ProductGroup> AllProductGroups;

        private ObservableCollection<ProductGroup> shownProductGroups;

        public ObservableCollection<ProductGroup> ShownProductGroups
        {
            get { return shownProductGroups; }
            set
            {
                shownProductGroups = value;
                NotifyPropertyChanged();
            }
        }

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
        public Contract SelectedContract
        {
            get { return selectedContract; }
            set
            {
                selectedContract = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<Customer> Customers { get; set; }

        public ContractViewModel(ObservableCollection<Customer> customers)
        {
            Customers = customers;

            contract = new Contract(true);
            productGroups = new ObservableCollection<ProductGroup>();
            StartDate = DateTime.Today;
            AllProductGroups = db.GetProductGroups();

            Contracts = db.Contracts(Customers, AllProductGroups);

            ContractManager = new ContractManager(new List<Contract>(Contracts));
            ShownProductGroups = new ObservableCollection<ProductGroup>(AllProductGroups);
            AddProductGroupCommand = new WpfCommand(AddProductGroupExecute, AddProductGroupCanExecute);
            RemoveProductGroupCommand = new WpfCommand(RemoveProductGroupExecute, RemoveProductGroupCanExecute);
            ClearContractCommand = new WpfCommand(ClearContractExecute, ClearContractCanExecute);
            SaveContractCommand = new WpfCommand(SaveContractExecute, SaveContractCanExecute);
            RemoveContractCommand = new WpfCommand(RemoveContractExecute, RemoveContractCanExecute);
            UpdateContractCommand = new WpfCommand(UpdateContractExecute, UpdateContractCanExecute);
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
                    case "Customer":
                        if (Customer == null)
                            return PropertyIsEmptyErrorMessage(propertyName);
                        break;
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
                            return "Der er ikke Tilføjet nogle produkt grupper";

                        break;

                    case "Discount":
                        if (String.IsNullOrEmpty(Discount))
                            return PropertyIsEmptyErrorMessage(propertyName);

                        int discount;
                        message = ValidateNumericParse<int>(Discount, propertyName, out discount);

                        if (message != null)
                            return message;

                        if((discount < 0) || (discount >= 99))
                            return "Rabaten skal være mellem 1-99";

                        contract.Discount = discount;
                        break;
                }
                return null;
            }
        }
        #endregion

        #region AddProductGroupCommand
        public ICommand AddProductGroupCommand { get; set; }

        public void AddProductGroupExecute(object parameter)
        {
            ProductGroups.Add(CbxSelectedProductGroup);
            ShownProductGroups.Remove(CbxSelectedProductGroup);
            NotifyPropertyChanged("ShownProductGroups");
            NotifyPropertyChanged("ProductGroups");
        }

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

        public ICommand ClearContractCommand { get; set; }

        public void ClearContractExecute(object parameter)
        {
            SelectedContract = null;
            ID = 0;
            Customer = null;
            StartDate = DateTime.Today;
            Status = false;
            Period = null;
            Discount = null;
            Subscription = false;
            ShownProductGroups = new ObservableCollection<ProductGroup>(AllProductGroups);
            ProductGroups = new ObservableCollection<ProductGroup>();
        }

        public bool ClearContractCanExecute(object parameter)
        {
            return true;
        }
        #endregion

        #region SaveContractCommand
        public ICommand SaveContractCommand { get; set; }

        public void SaveContractExecute(object parameter)
        {
            Contract contractClone = contract.Clone();
            string message;
            SetStatus();
            if (selectedContract == null)
            {
                contract.AutoAssignId(Contracts);
                contract.ProductGroups = ProductGroups.ToList();
                if (db.AddContract(contract))
                {
                    db.AddContract(contract);
                    ContractManager.AddContract(contractClone);
                    Contracts.Add(contractClone);
                    NotifyPropertyChanged("Contracts");
                    message = String.Format("Aftale med ID: {0} blev oprettet", contract.ID);
                    db.LogAdd(message);
                    MessageBox.Show(message);
                }
                else
                {
                    message = String.Format("Fejl! Aftale med ID: {0} eksistere allerede", contract.ID);
                    db.LogAdd(message);
                    MessageBox.Show("Fejl! Aftale eksisterer allerede!");
                }

            }
            else if (selectedContract != null)
            {
                db.UpdateContract(contract);
                ContractManager.RemoveContract(selectedContract);
                ContractManager.AddContract(contractClone);
                Contracts.Remove(selectedContract);
                Contracts.Add(contractClone);
                NotifyPropertyChanged("Contracts");
                message = String.Format("Aftale med ID: {0} blev redigeret", contract.ID);
                db.LogAdd(message);
                MessageBox.Show(message);
            }
        }

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

        public void RemoveContractExecute(object parameter)
        {
            ID = selectedContract.ID;
            ContractManager.RemoveContract(selectedContract);
            db.RemoveContract(selectedContract);
            Contracts.Remove(selectedContract);
            NotifyPropertyChanged("Contracts");
            db.LogAdd(String.Format("Kontrakt med ID: {0} blev slettet", ID));
            MessageBox.Show("Kontrakt Slettet");
        }
        public bool RemoveContractCanExecute(object paramter)
        {
            if (SelectedContract == null)
                return false;
            else
                return true;
        }
        #endregion

        #region UpdateContractCommand
        public ICommand UpdateContractCommand { get; set; }

        public void UpdateContractExecute(object parameter)
        {
            ID = selectedContract.ID;
            Customer = selectedContract.Customer;
            Period = selectedContract.Period.ToString();
            StartDate = selectedContract.StartDate;
            Status = selectedContract.Status;
            Discount = selectedContract.Discount.ToString();
            Subscription = selectedContract.Subscription.Status;
            ProductGroups = new ObservableCollection<ProductGroup>(selectedContract.ProductGroups);
            ShownProductGroups = new ObservableCollection<ProductGroup>(AllProductGroups.Except(ProductGroups).ToList());
            contract.Discount = selectedContract.Discount;
        }

        public bool UpdateContractCanExecute(object paramter)
        {
            if (SelectedContract == null)
                return false;
            else
                return true;
        }
        #endregion
    }
}
