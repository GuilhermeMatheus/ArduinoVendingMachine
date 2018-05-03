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
        OperationResult UpdateClientIpEndPoint(Machine machine, IPEndPoint machineIpEndPoint);
        OperationResult UpdateAccessPointIp(Machine machine, IPAddress ip);
        OperationResult CreateUpdateMachinePriceTableJob(Machine machine);
    }
}
