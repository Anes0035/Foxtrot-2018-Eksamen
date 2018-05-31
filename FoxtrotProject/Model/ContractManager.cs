using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxtrotProject.Model
{
    // Author Christian
    class ContractManager
    {
        private List<Contract> Contracts { get; set; }

        public ContractManager(List<Contract> contracts)
        {
            Contracts = contracts;
            Count(new ContractEventArgs(Contracts));
        }

        public event EventHandler<ContractEventArgs> onContractsChange;

        public void AddContract(Contract contract)
        {
            Contracts.Add(contract);
            Count(new ContractEventArgs(Contracts));
        }

        public void RemoveContract(Contract contract)
        {
            Contracts.Remove(contract);
            Count(new ContractEventArgs(Contracts));
        }

        protected virtual void Count(ContractEventArgs e)
        {
            onContractsChange?.Invoke(this, e);
        }

    }
}
