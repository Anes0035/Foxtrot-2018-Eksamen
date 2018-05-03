using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxtrotProject.Model
{
    struct ContactPerson
    {
        string Name;
        int TelephoneNumber;
    }
    class Customer
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public int TelephoneNumber { get; set; }

        public ContactPerson ContactPerson { get; set; }

        public double GrossIncome { get; set; }

        public int CVR { get; set; }


    }
}
