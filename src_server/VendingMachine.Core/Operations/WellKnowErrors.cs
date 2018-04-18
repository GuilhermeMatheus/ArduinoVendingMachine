using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Core.Operations
{
    public enum WellKnowErrors
    {
        ClientNotFound = 1,
        ClientWithNoEnoughCredit = 2,
        InvalidProduct = 3,
        InvalidPrice = 4
    }
}
