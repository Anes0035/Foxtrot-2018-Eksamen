using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxtrotProject.Model
{
    class ProductManager
    {
        public List<Product> products { get; set; }

        public ProductManager()
        {
            products = new List<Product>();
        }
    }
}
