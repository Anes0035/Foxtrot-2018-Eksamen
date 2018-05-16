using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxtrotProject.Model
{
    class ProductSearchFunction
    {
        public ObservableCollection<Product> SearchProductName(ObservableCollection<Product> products, string searchString)
        {
            return (ObservableCollection<Product>) products.Where(p => p.Name.Contains(searchString));
        }
        public ObservableCollection<Product> SearchProductDescription(ObservableCollection<Product> products, string searchString)
        {
            return (ObservableCollection<Product>)products.Where(p => p.Description.Contains(searchString));
        }
        public ObservableCollection<Product> SearchProductID(ObservableCollection<Product> products, string searchString)
        {
            return (ObservableCollection<Product>)products.Where(p => p.ID.ToString().Contains(searchString));
        }
    }
}
