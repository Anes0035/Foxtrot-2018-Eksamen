using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxtrotProject.Model
{
    class Statistic
    {
        //Author Christian and Anes
        private Dictionary<ProductGroup, int> ProductGroupCount;

        public int X { get; set; }

        public Statistic(ContractManager contractManager)
        {
            ProductGroupCount = new Dictionary<ProductGroup, int>();
            contractManager.onContractsChange += new EventHandler<ContractEventArgs>(CountProductGroup);
        }

        private void CountProductGroup(object sender, ContractEventArgs e)
        {
            ProductGroupCount = new Dictionary<ProductGroup, int>();

            foreach (Contract c in e.Contracts)
            {
                foreach(ProductGroup pg in c.ProductGroups)
                {
                    if (!ProductGroupCount.ContainsKey(pg))
                        ProductGroupCount.Add(pg, 1);
                    else
                        ProductGroupCount[pg]++;
                }

            }
            //Sort method Anes 
        }

        public ObservableCollection<string> FindTopXProducts()
        {
            Dictionary<ProductGroup, int> TopProductGroupCount = (from pair in ProductGroupCount orderby pair.Value descending select pair).Take(X).ToDictionary(pair => pair.Key, pair => pair.Value);
            ObservableCollection<string> TopProductGroups = new ObservableCollection<string>();

            int counter = 0;
            foreach(KeyValuePair<ProductGroup, int> p in TopProductGroupCount)
            {
                counter++;
                TopProductGroups.Add(String.Format("#{0}. {1} har {2} forekomst(er)", counter, p.Key, p.Value));

            }

            return TopProductGroups;
        }
    }
}

