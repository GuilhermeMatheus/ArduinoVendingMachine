using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Core.Model;
using VendingMachine.Core.Operations;

namespace VendingMachine.Core.Services
{
    public interface IRemoteMachineService
    {
        Task<OperationResult> UpdateRemoteMachinePriceTableAsync(Machine machine, IEnumerable<ProductRail> products);
    }
}
