﻿using FoxtrotProject.Model;
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

        private Product currentProduct = new Product();


        private int iD;

        public int ID
        {
            get { return currentProduct.ID; }
            set
            {
                currentProduct.ID = value;
                NotifyPropertyChanged();
            }
        }

        private string name;

        public string Name
        {
            get { return currentProduct.Name; }
            set
            {
                currentProduct.Name = value;
                NotifyPropertyChanged();
            }
        }

        private string description;

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

        private string category;

        public string Category
        {
            get { return currentProduct.Category; }
            set
            {
                currentProduct.Category = value;
                NotifyPropertyChanged();
            }
        }


        #endregion

        //private Database db = new Database();

        public ProductViewModel()
        {

            db = new Database();
            currentProduct = new Product();

            Products = db.Products();

            SaveProductCommand = new WpfCommand(SaveProductExecute, SaveProductCanExecute);
            

         //   RemoveProductCommand = new WpfCommand(RemoveProductExecute, RemoveProductCanExecute);

        }


        #region SaveProductCommand 

            
        public ICommand SaveProductCommand { get; set; }

        public void SaveProductExecute(object parameter)
        {
            Products.Add(currentProduct.Clone());
            db.AddProduct(currentProduct.Clone());
            NotifyPropertyChanged("products");
            MessageBox.Show("Product Oprettet");

        }

        public bool SaveProductCanExecute(object parameter)
        {
            if (FirstErrorMessage != null)
                return false;
            else
                return true;
        }



        #endregion


        #region IDataErrorInfo
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

        public string Error
        {
            get { return null; }
        }

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

        #region RemoveProductExecute
        public ICommand RemoveProductCommand { get; set; }

        public void RemoveProductExecute(object parameter)
        {
            
            Products.Remove(currentProduct.Clone());
            db.RemoveProduct(iD);
            NotifyPropertyChanged("Product");
            MessageBox.Show("Product Slettet");


        }
        public bool RemoveProductCanExecute(object parameter)
        {
            return true;
        }
        #endregion

        #region EditProductExecute

        public void EditProductExecute(object parameter)
        {

            //Products.Add(currentProduct.Clone());
            //db.RemoveProduct(iD);
            //NotifyPropertyChanged("Product");
            //MessageBox.Show("Product redigeret");


        }
        public bool EditProductCanExecute(object parameter)
        {

            if (FirstErrorMessage != null)
                return false;
            else
                return true;
        }



        #endregion
    }
}
