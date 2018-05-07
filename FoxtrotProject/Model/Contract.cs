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

        private List<Tuple<ProductGroup, int>> ProductGroupWithDiscount;

        public Subscription Subscription { get; set; }

        public int? GetDiscount(ProductGroup productGroup)
        {
            foreach(Tuple<ProductGroup, int> discount in ProductGroupWithDiscount)
            {
                if (productGroup == discount.Item1)
                    return discount.Item2;
            }
            return null;
        }

    }
}
