﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxtrotProject.Model
{

    // Author Christian and Elena
    class ContractEventArgs : EventArgs
    {
        public List<Contract> Contracts { get; set; }

        public ContractEventArgs(List<Contract> contracts)
        {
            this.Contracts = contracts;
        }
    }
}
