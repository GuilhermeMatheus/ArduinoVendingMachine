using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using VendingMachine.Core.Operations;
using VendingMachine.Core.Repository;
using VendingMachine.Core.Services;
using VendingMachine.Infrastructure.Actions;
using VendingMachine.Infrastructure.Helpers;

namespace VendingMachine.Infrastructure.ActionHandler
{
    public enum StartEventResult
    {
        Ok = 0b10000000,
        NotFound= 0b01000000
    }

    public class MachineStartupActionHandler : IActionHandler
    {
        private readonly IVendingMachineControlService _machineControlService;
        private readonly IMachineRepository _machineRepository;
        private readonly ILogger<MachineStartupActionHandler> _logger;

        public MachineStartupActionHandler(
            IMachineRepository machineRepository,
            IVendingMachineControlService machineControlService,
            ILogger<MachineStartupActionHandler> logger)
        {
            _machineRepository = machineRepository ?? throw new ArgumentNullException(nameof(machineRepository));
            _machineControlService = machineControlService ?? throw new ArgumentNullException(nameof(machineControlService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public byte[] Process(ActionContext context)
        {
            var machineIpEndPoint = (IPEndPoint)context.IncomingMessage["machine:IPEndPoint"];
            var rawBytes = context.IncomingMessage.RawBytes.AsSpan();

            // var machineAccessPoint = ByteHelper.GetMachineAccessPointIP(rawBytes.Slice(3));
            var machineId = ByteHelper.GetMachineId(rawBytes, 1);

            _logger.LogTrace($"machine:IPEndPoint is {machineIpEndPoint}");
            _logger.LogTrace($"machineId is {machineId}");

            var machine = _machineRepository.Get(machineId);
            if (machine == null)
                return new byte[] { (byte)StartEventResult.NotFound };

            var operationResult = OperationResult.Success;

            // operationResult += _machineControlService.UpdateAccessPointIp(machine, machineAccessPoint);
            operationResult += _machineControlService.UpdateClientIpEndPoint(machine, machineIpEndPoint);

            if (!operationResult.Succeeded)
            {
                //TODO: Check operationResult errors
                throw new NotImplementedException($"machineControlService failed to update machine '{machineId}' ip '{machineIpEndPoint}'");
            }

            return new byte[] { (byte)StartEventResult.Ok };
        }
    }
}
