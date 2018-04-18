using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Core.Operations
{
    internal static class OperationErrorFactory
    {
        public static OperationError FromWellKnowErrors(WellKnowErrors error) =>
            new OperationError((int)error, error.ToString());
    }
}
