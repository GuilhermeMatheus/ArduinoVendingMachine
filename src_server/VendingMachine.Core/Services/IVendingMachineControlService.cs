using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using VendingMachine.Core.Model;
using VendingMachine.Core.Operations;

namespace VendingMachine.Core.Services
{
    public interface IVendingMachineControlService
    {
        OperationResult UpdateMachineIp(Machine machine, IPEndPoint machineIpEndPoint);
        OperationResult CreateUpdateMachinePriceTableJob(Machine machine);
    }
}
