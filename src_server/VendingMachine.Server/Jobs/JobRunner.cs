using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using VendingMachine.Core.Repository;
using VendingMachine.Core.Model;
using Microsoft.Extensions.Logging;
using VendingMachine.Core.Helpers;
using VendingMachine.Core.Services;
using VendingMachine.Core.Operations;
using System.Threading.Tasks;

namespace VendingMachine.Infrastructure.Jobs
{
    public class JobRunner
    {
        private readonly IJobRepository _jobRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMachineRepository _machineRepository;
        private readonly IRemoteMachineService _remoteMachineService;
        private readonly ILogger _logger;

        public JobRunner(
            IJobRepository jobRepository, 
            IProductRepository productRepository,
            IMachineRepository machineRepository,
            IRemoteMachineService remoteMachineService,
            ILogger<JobRunner> logger)
        {
            _jobRepository = jobRepository ?? throw new ArgumentNullException(nameof(jobRepository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _machineRepository = machineRepository ?? throw new ArgumentNullException(nameof(machineRepository));
            _remoteMachineService = remoteMachineService ?? throw new ArgumentNullException(nameof(remoteMachineService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task RunAsync()
        {
            var now = DateTime.Now;
            var jobs =
                _jobRepository.Query.Where(j =>
                    j.ScheduledDateTime <= now &&
                    j.ExecutionDateTime == null &&
                    j.JobType == JobType.UpdateMachineProductTable
                );

            var job = jobs.FirstOrDefault();
            if (job == null)
                return;

            _logger.LogInformationIfEnabled(() => $"Found {jobs.Count()} UpdateMachineProductTable jobs to run.");

            _logger.LogInformationIfEnabled(() => $"Running job {job.Id} with data '{job.Data}'");
            job.ExecutionDateTime = DateTime.Now;

            if (!int.TryParse(job.Data, out int machineId))
                throw new InvalidOperationException($"Unexpected data '{job.Data}' to UpdateMachineProductTable JobType");

            var machine = _machineRepository.Get(machineId);
            var products = _productRepository.GetProductsInMachine(machine);

            var result = await _remoteMachineService.UpdateRemoteMachinePriceTableAsync(machine, products);

            if(result.Errors.Any())
            {
                _logger.LogCriticalIfEnabled(() => $"Failed to run job {job.Id} with {result.Errors.Count()} errors.");
                _logger.LogCriticalIfEnabled(
                    () => 
                        result.Errors
                            .Select(_ => _.ToString())
                            .Aggregate((f1,f2) => $"{f1}\n{f2}")
                );
            }

            await _jobRepository.SaveAsync();
        }
    }
}
