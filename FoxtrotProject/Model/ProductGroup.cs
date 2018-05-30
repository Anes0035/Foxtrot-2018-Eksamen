using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxtrotProject.Model
{
    class ProductGroup
    {
        public string Name { get; set; }

        public List<Product> Products { get; set; }

        public string Category { get; set; }

        public ProductGroup()
        {
            Products = new List<Product>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
