﻿using System;
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

        public decimal Price { get; set; }

        public string Category { get; set; }

         public Product(int iD)
          {
              ID = iD;
          }

          public Product(List<Product> products, string name, string description, decimal price, string category)
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

