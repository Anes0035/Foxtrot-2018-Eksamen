using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxtrotProject.Model
{
    class Customer
    {
        private int Cvr;

        public int cvr
        {
            get { return cvr; }
            set { cvr = value; }
        }



        private string Name;

        public string name
        {
            get { return name; }
            set { name = value; }
        }


        private string Address;

        public string address
        {
            get { return address; }
            set { address = value; }
        }


        private int TelefonNr;

        public int telefonNr
        {
            get { return telefonNr; }
            set { telefonNr = value; }
        }


        private string CustomerInfo;

        public string customerInfo
        {
            get { return customerInfo; }
            set { customerInfo = value; }
        }


        private string ContactPerson;

        public string contactPerson
        {
            get { return contactPerson; }
            set { contactPerson = value; }
        }


        private int GrossIncome;

        public int grossIncome
        {
            get { return grossIncome; }
            set { grossIncome = value; }
        }

    }
}
