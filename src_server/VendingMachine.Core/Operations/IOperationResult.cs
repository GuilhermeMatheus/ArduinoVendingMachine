using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Core.Operations
{
    public class OperationResult<TResult> 
    {
        public bool Success { get; }
        public TResult Result { get; }

        public OperationResult(bool sucess, TResult result)
        {
            Success = sucess;
            Result = result;
        }
    }
}
