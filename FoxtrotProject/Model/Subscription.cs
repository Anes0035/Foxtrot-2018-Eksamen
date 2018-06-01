using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxtrotProject.Model
{
    //Author Anes
    class Subscription
    {
        public bool Status { get; set; }

        public override string ToString()
        {
            if (Status == true)
                return "Aktiv";
            else
                return "Inaktiv";
        }
    }
}
