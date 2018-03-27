using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Core.Operations;

namespace VendingMachine.Core.Services
{
    public interface ISaleService
    {
        OperationResult<SaleOperationResult> Sale(SaleOperation sale);
    }
}
