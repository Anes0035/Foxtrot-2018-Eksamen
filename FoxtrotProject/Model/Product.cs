using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FoxtrotProject.Model
{
    class Product
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string Category { get; set; }

        public Product()
        {

        }

         public Product(int iD)
          {
              ID = iD;
          }

        public Product Clone()
        {
            return new Product() { ID = ID, Name = Name, Description = Description, Price = Price, Category = Category };
        }

          public Product(List<Product> products, string name, string description, double price, string category)
          {
              AutoAssignId(products);
              name = Name;
              description = Description;
              price = Price;
              category = Category;
          }

          private void AutoAssignId(List<Product> products)
          {
              for(int i = 1; products.Where(p => p.ID == i).Any(); i++)
              {
                  ID = i;
              }
          } 


     

          
    }

}

