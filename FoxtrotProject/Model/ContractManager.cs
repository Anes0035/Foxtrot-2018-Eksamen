using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxtrotProject.Model
{
    class ContractManager
    {
        private List<Contract> Contracts { get; set; }

        public void AddContract(Contract contract)
        {
            Contracts.Add(contract);
            Count(new ContractEventArgs(contract));
        }

        public event EventHandler<ContractEventArgs> onAddContract;

        protected virtual void Count(ContractEventArgs e)
        {
            onAddContract?.Invoke(this, e);
        }

    }
}
