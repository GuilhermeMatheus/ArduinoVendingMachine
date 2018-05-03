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
        private readonly IJobRepository _jobRepository;

        public VendingMachineControlService(IMachineRepository machineRepository, IJobRepository jobRepository)
        {
            _machineRepository = machineRepository ?? throw new ArgumentNullException(nameof(machineRepository));
            _jobRepository = jobRepository ?? throw new ArgumentNullException(nameof(jobRepository));
        }

        public OperationResult CreateUpdateMachinePriceTableJob(Machine machine)
        {
            if (machine == null)
                throw new ArgumentNullException(nameof(machine));

            var job = new Job
            {
                JobType = JobType.UpdateMachineProductTable,
                ScheduledDateTime = DateTime.MinValue,
                Data = machine.Id.ToString()
            };

            _jobRepository.Add(job);
            _jobRepository.Save();

            return OperationResult.Success;
        }

        public OperationResult UpdateAccessPointIp(Machine machine, IPAddress ip)
        {
            machine.AccessPointIp = ip.ToString();
            _machineRepository.Save();

            return OperationResult.Success;
        }

        public OperationResult UpdateClientIpEndPoint(Machine machine, IPEndPoint machineIpEndPoint)
        {
            machine.IPEndPoint = machineIpEndPoint.ToString();
            _machineRepository.Save();

            return OperationResult.Success;
        }
    }
}
