using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Core.Operations
{
    public class OperationError
    {
        public int Code { get; }
        public string Description { get; }

        public OperationError(int code, string description)
        {
            Code = code;
            Description = description;
        }

        public override string ToString()
            => $"{Code}: {Description}";
    }
}
