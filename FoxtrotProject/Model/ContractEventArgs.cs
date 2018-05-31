using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxtrotProject.Model
{

    // Author Christian and Elena
    class ContractEventArgs : EventArgs
    {
        public Contract contract { get; set; }

        public ContractEventArgs(Contract contract)
        {
            this.contract = contract;
        }
    }
}
