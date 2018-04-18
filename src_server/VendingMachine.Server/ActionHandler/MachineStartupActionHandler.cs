using System;
using System.Collections.Generic;
using System.Text;
using VendingMachine.Core.Services;
using VendingMachine.Server.Actions;

namespace VendingMachine.Server.ActionHandler
{
    public class MachineStartupActionHandler : IActionHandler
    {
        private readonly IVendingMachineControlService _machineControlService;

        public MachineStartupActionHandler(IVendingMachineControlService machineControlService) 
        {
            _machineControlService = machineControlService ?? throw new ArgumentNullException(nameof(machineControlService));
        }

        public byte[] Process(ActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
