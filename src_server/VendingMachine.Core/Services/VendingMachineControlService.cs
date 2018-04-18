using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using VendingMachine.Core.Model;
using VendingMachine.Core.Operations;
using VendingMachine.Core.Repository;

namespace VendingMachine.Core.Services
{
    public class VendingMachineControlService : IVendingMachineControlService
    {
        private readonly IMachineRepository _machineRepository;

        public VendingMachineControlService(IMachineRepository machineRepository)
        {
            _machineRepository = machineRepository ?? throw new ArgumentNullException(nameof(machineRepository));
        }

        public OperationResult UpdateMachineIp(Machine machine, IPEndPoint machineIpEndPoint)
        {
            machine.IPEndPoint = machineIpEndPoint.ToString();
            _machineRepository.Save();

            return OperationResult.Success;
        }
    }
}
