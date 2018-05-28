using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxtrotProject.Model
{
    class Statistic
    {
        private Dictionary<ProductGroup, int> ProductGroupCount;

        private ContractManager contractManager;

        public Statistic(ContractManager contractManager)
        {
            this.contractManager = contractManager;
            ProductGroupCount = new Dictionary<ProductGroup, int>();
            contractManager.onAddContract += new EventHandler<ContractEventArgs>(CountProductGroup);
        }

        private void CountProductGroup(object sender, ContractEventArgs e)
        {
            foreach (ProductGroup pg in e.contract.ContractGroups)
            {
                if (!ProductGroupCount.ContainsKey(pg))
                    ProductGroupCount.Add(pg, 1);
                else
                    ProductGroupCount[pg]++;
            }
            //Sort method Anes 
            ProductGroupCount = GetSortedDictionary(ProductGroupCount);
        }

        private Dictionary<ProductGroup, int> GetSortedDictionary(Dictionary<ProductGroup, int> dictionary)
        {
            return (Dictionary<ProductGroup, int>) from pair in dictionary orderby pair.Value descending select pair;
        }

        public List<Product> FindTopXProducts(int x, List<Contract> contracts)
        {
            throw new NotImplementedException();
        }
    }
}

