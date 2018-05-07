using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxtrotProject.Model
{
    class ContractEventArgs : EventArgs
    {
        public Contract contract { get; set; }

        public ContractEventArgs(Contract contract)
        {
            this.contract = contract;
        }
    }
}
