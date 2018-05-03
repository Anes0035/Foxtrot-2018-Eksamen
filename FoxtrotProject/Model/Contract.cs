using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxtrotProject.Model
{
    class Contract
    {
        public DateTime StartDate { get; set; }

        public int Period { get; set; }

        public bool Status { get; set; }

        public List<ProductGroup> ProductGroups { get; set; }

        public Subscription Subscription { get; set; }

        // need a possible discount for each productgroup
        public int Discount { get; set; }
    }
}
