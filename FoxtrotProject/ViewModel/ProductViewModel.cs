using FoxtrotProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxtrotProject.ViewModel
{
    class ProductViewModel : ViewModel
    {
        private Product currentProduct;

        private Database Db;

        public ObservableCollection<ProductGroup> ProductGroups { get; set; }

        private List<Product> products;

        public List<Product> Products
        {
            get { return products; }
            set { products = value; }
        }

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

        private decimal price;

        public decimal Price
        {
            get { return currentProduct.Price; }
            set
            {
                currentProduct.Price = value;
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


        public void AddProduct(object parameter)
        {
            //Product product = new Product();

        }   
        
    }
}
