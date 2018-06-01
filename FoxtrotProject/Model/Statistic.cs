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

        private ContractManager contractManager;
        //Author Christian and Anes
        public Statistic(ContractManager contractManager)
        {
            ProductGroupCount = new Dictionary<ProductGroup, int>();
            this.contractManager = contractManager;
        }
        //Author Christian and Anes
        private void CountProductGroup()
        {
            ProductGroupCount = new Dictionary<ProductGroup, int>();

            foreach (Contract c in contractManager.Contracts)
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
        //Author Christian and Anes
        public ObservableCollection<string> FindTopXProducts()
        {
            CountProductGroup();
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

