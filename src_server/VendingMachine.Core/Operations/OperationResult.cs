using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Core.Operations
{
    public class OperationResult
    {
        public bool Succeeded { get; }
        public IEnumerable<OperationError> Errors { get; }

        private OperationResult(bool succeeded, IEnumerable<OperationError> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToList();
        }

        public static OperationResult Success { get; } 
            = new OperationResult(true, Enumerable.Empty<OperationError>());

        public static OperationResult Failed(params OperationError[] errors)
            => new OperationResult(false, errors);

        public static OperationResult operator +(OperationResult left, OperationResult right)
        {
            if (left.Succeeded)
                return right;

            if (right.Succeeded)
                return left;

            var errors = left.Errors.Concat(right.Errors);
            return new OperationResult(false, errors);
        }

    }
}
