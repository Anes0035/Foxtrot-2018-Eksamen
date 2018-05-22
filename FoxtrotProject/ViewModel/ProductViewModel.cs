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
      

        private Database Db = new Database();

        public ObservableCollection<ProductGroup> ProductGroups { get; set; }

        public ObservableCollection<Product> Products { get; set; }

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


        public void AddProduct()
        {
            Product Clone = currentProduct.Clone();
            Db.AddProduct(Clone);
            Products.Add(Clone);
        }

        public Collection<Product> _Search_Product { get; set; }

        public void Search_Product(Collection<Product> _p)
        {
            for (int i = 0; _p.Where(p => p.ID == i).Any(); i++)
            {
                ID = i;
            }

        }




        public void DeleteProduct()
        {
            Db.RemoveProduct(ID);
        }

        public void UpDateProduct()
        {
            Product Clone = currentProduct.Clone();
            Db.UpDateProduct(Clone);
            Products.Add(Clone);
        }
    }
}
