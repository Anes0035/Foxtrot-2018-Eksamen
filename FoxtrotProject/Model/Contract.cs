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

        public Dictionary<ProductGroup, int> DiscountGroups { get; set; }

        public Subscription Subscription { get; set; }

        public int Discount { get; set; }
    }
}
