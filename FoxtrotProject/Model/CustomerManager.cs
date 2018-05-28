using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxtrotProject.Model
{
    class CustomerManager
    {
        public List<Customer> customers { get; set; }
        public CustomerManager()
        {
            customers = new List<Customer>();
        }      
    }
}
