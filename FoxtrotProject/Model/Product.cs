using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FoxtrotProject.Model
{
    class Product
    {
        //Authors: Elena And Christian
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string Category { get; set; }

        public Product SelectedProduct { get; set; }
        public Product()
        {

        }

        public Product(int iD)
        {
            ID = iD;
        }

        // Author Elena
        public Product Clone()
        {
            return new Product() { ID = ID, Name = Name, Description = Description, Price = Price, Category = Category };
        }

        public Product(ObservableCollection<Product> products, string name, string description, double price, string category)
        {
            AutoAssignId(products);
            name = Name;
            description = Description;
            price = Price;
            category = Category;
        }

        // Author Elena
        public void AutoAssignId(ObservableCollection<Product> products)
        {
            int counter = 0;
            do
            {
                counter++;
                ID = counter;
            }
            while (products.Where(p => p.ID == counter).Any());

        }






    }

}

