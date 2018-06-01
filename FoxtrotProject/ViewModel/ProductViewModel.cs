using FoxtrotProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FoxtrotProject.ViewModel
{
    class ProductViewModel : ViewModel, IDataErrorInfo
    {
        #region Product

        
        ProductManager productManager;

        public ObservableCollection<ProductGroup> ProductGroups { get; set; }

        private ObservableCollection<Product> products;
        public ObservableCollection<Product> Products
        {
            get { return products; }
            set
            {
                products = value;
                NotifyPropertyChanged();
            }
        }

        public Product currentProduct;




        public int ID
        {
            get { return currentProduct.ID; }
            set
            {
                currentProduct.ID = value;
                NotifyPropertyChanged();
            }
        }



        public string Name
        {
            get { return currentProduct.Name; }
            set
            {
                currentProduct.Name = value;
                NotifyPropertyChanged();
            }
        }



        public string Description
        {
            get { return currentProduct.Description; }
            set
            {
                currentProduct.Description = value;
                NotifyPropertyChanged();
            }
        }

        private string price;

        public string Price
        {
            get { return price; }
            set
            {
                price = value;
                NotifyPropertyChanged();
            }
        }



        public string Category
        {
            get { return currentProduct.Category; }
            set
            {
                currentProduct.Category = value;
                NotifyPropertyChanged();
            }
        }

        private Product selectedproduct;

        public Product SelectedProduct
        {
            get { return selectedproduct; }
            set
            {
                selectedproduct = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        //private Database db = new Database();

        // Author Elena and Kasper
        public ProductViewModel()
        {
            productManager = new ProductManager();
            db = new Database();
            currentProduct = new Product();
            productManager.products = db.Products();

            products = new ObservableCollection<Product>(productManager.products);
            InitializeSearchOptions();

            SaveProductCommand = new WpfCommand(SaveProductExecute, SaveProductCanExecute);
            RemoveProductCommand = new WpfCommand(RemoveProductExecute, RemoveProductCanExecute);
            EditProductCommand = new WpfCommand(EditProductExecute, EditProductCanExecute);

            SearchProductCommand = new WpfCommand(SearchProductExecute, SearchProductCanExecute);

            ClearProductCommand = new WpfCommand(ClearProductExecute, ClearProductCanExecute);
        }
        #region IDataErrorInfo
        // Author Elena
        public string FirstErrorMessage
        {
            get
            {
                PropertyInfo[] properties = GetType().GetProperties();
                foreach (PropertyInfo p in properties)
                {
                    if (this[p.Name] != null)
                        return this[p.Name];
                }

                return null;
            }
        }
        // Author Elena
        public string Error
        {
            get { return null; }
        }
        // Author Elena
        public string this[string propertyName]
        {
            get
            {
                string message;
                switch (propertyName)
                {


                    case "Name":
                        if (String.IsNullOrEmpty(Name))
                            return PropertyIsEmptyErrorMessage("Navn");
                        break;


                    case "Description":
                        if (String.IsNullOrEmpty(Description))
                            return PropertyIsEmptyErrorMessage("Description");
                        break;


                    case "Price":
                        if (String.IsNullOrEmpty(Price))
                            return PropertyIsEmptyErrorMessage("Price");


                        double price;
                        message = ValidateNumericParse<double>(Price, propertyName, out price);

                        if (message != null)
                            return message;

                        currentProduct.Price = price;
                        break;


                    case "Category":
                        if (String.IsNullOrEmpty(Category))
                            return PropertyIsEmptyErrorMessage("Category");
                        break;

                }

                return null;
            }
        }

        #endregion

        #region SaveProductCommand 


        public ICommand SaveProductCommand { get; set; }
        // Author Elena and Kasper
        public void SaveProductExecute(object parameter)
        {
            if (selectedproduct == null)
            {
                currentProduct.AutoAssignId(products);
                if (db.AddProduct(currentProduct))
                {
                    Products.Add(currentProduct.Clone());
                    NotifyPropertyChanged("products");
                    db.LogAdd(String.Format("Produktet med ID: {0} blev oprettet", currentProduct.ID));
                    MessageBox.Show("Produkt Oprettet");
                }
                else
                {
                    MessageBox.Show("Produktet eksisterer allerede!");
                }
            }

            if (selectedproduct != null)
            {
                db.EditProduct(currentProduct.Clone());
                Products.Remove(selectedproduct);
                Products.Add(currentProduct.Clone());

                NotifyPropertyChanged("products");
                db.LogAdd(String.Format("Produktet med ID: {0} blev redigeret", currentProduct.ID));
                MessageBox.Show("Produkt rettet");
                currentProduct.SelectedProduct = null;
            }
        }
        // Author Elena
        public bool SaveProductCanExecute(object parameter)
        {
            if (FirstErrorMessage != null)
                return false;
            else
                return true;
        }



        #endregion      

        #region RemoveProductExecute
        public ICommand RemoveProductCommand { get; set; }
        // Author Elena and Kasper
        public void RemoveProductExecute(object parameter)
        {
            currentProduct.ID = selectedproduct.ID;
            db.RemoveProduct(selectedproduct);
            Products.Remove(selectedproduct);
            NotifyPropertyChanged("products");
            db.LogAdd(String.Format("Produktet med ID: {0} blev slettet", currentProduct.ID));
            MessageBox.Show("Produkt Slettet");


        }
        public bool RemoveProductCanExecute(object parameter)
        {
            if (selectedproduct == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region EditProductExecute
        public ICommand EditProductCommand { get; set; }
        // Author Elena and Kasper
        public void EditProductExecute(object parameter)
        {

            ID = selectedproduct.ID;
            Name = selectedproduct.Name;
            Description = selectedproduct.Description;
            Price = selectedproduct.Price.ToString();
            Category = selectedproduct.Category;

        }
        // Author Elena 
        public bool EditProductCanExecute(object parameter)
        {

            if (selectedproduct == null)
                return false;
            else
                return true;
        }
        #endregion

        #region SearchProductCommand
        public ICommand SearchProductCommand { get; set; }
        // Author Elena 
        public ObservableCollection<string> SearchOptions { get; set; }

        public string SelectedSearchOption { get; set; }
        // Author Elena 
        private void InitializeSearchOptions()
        {
            SearchOptions = new ObservableCollection<string>();
            SearchOptions.Add("Starter med");
            SearchOptions.Add("Indeholder");
            SelectedSearchOption = SearchOptions[0];
        }
        // Author Elena
        public string SearchProduct { get; set; }
        // Author Elena 
        public void SearchProductExecute(object parameter)
        {
            try
            {
                products = new ObservableCollection<Product>(productManager.products);

                switch (SelectedSearchOption)
                {
                    case "Starter med":
                        foreach (Product p in products.ToList())
                        {

                            if (!p.ID.ToString().ToLower().StartsWith(SearchProduct.ToLower()) && !p.Name.ToString().ToLower().StartsWith(SearchProduct.ToLower())
                               && (p.Description != null ? !p.Description.ToString().ToLower().StartsWith(SearchProduct.ToLower()) : true))
                            {
                                products.Remove(p);

                            }

                        }
                        break;
                    case "Indeholder":
                        foreach (Product p in products.ToList())
                        {

                            if (!p.ID.ToString().ToLower().Contains(SearchProduct.ToLower()) && !p.Name.ToString().ToLower().Contains(SearchProduct.ToLower())
                               && (p.Description != null ? !p.Description.ToString().ToLower().Contains(SearchProduct.ToLower()) : true))
                            {
                                products.Remove(p);

                            }

                        }
                        break;
                }

                NotifyPropertyChanged("products");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
        }
        // Author Elena 
        public bool SearchProductCanExecute(object parameter)
        {
            return true;
        }
        #endregion

        #region  ClearProductCommand
        public ICommand ClearProductCommand { get; set; }
        // Author Elena
        public void ClearProductExecute(object parameter)
        {

            ID = 0;
            Name = "";
            Description = "";
            Price = "";
            Category = "";

            //txtProductID.Text = string.Empty;
            //txtPName.Text = string.Empty;
            //txtDescription.Text = string.Empty;
            //txtPrice.Text = null;
            //txtProductCategory.Text = string.Empty;
        }
        // Author Elena
        public bool ClearProductCanExecute(object parameter)
        {
            return true;

        }
        #endregion

    }
}
