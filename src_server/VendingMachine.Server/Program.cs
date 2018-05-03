using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Core.Helpers;
using VendingMachine.Core.Repository;
using VendingMachine.Core.Services;
using VendingMachine.EF;
using VendingMachine.EF.Repository;
using VendingMachine.Infrastructure.Actions;
using VendingMachine.Infrastructure.Exceptions;
using VendingMachine.Infrastructure.Jobs;
using VendingMachine.Infrastructure.Remote;
using VendingMachine.Infrastructure.Request;

[assembly: InternalsVisibleTo("VendingMachine.Infrastructure.Tests")]
namespace VendingMachine.Infrastructure
{
    class Program
    {
        private static ILogger<Program> _logger;

        private static void Main(string[] args)
        {
            Console.WriteLine("Initializing server. . .");
            var container = GetContainer();
            var seeded = false;

            while(true)
                using (var scope = container.BeginLifetimeScope())
                {
                    _logger = scope.Resolve<ILogger<Program>>();

                    if (!seeded)
                    {
                        var seeder = new SampleDataSeeder();
                        seeder.Initialize(scope.Resolve<VendingMachineDbContext>()).Wait();
                        seeded = true;
                    }

                    var requestListener = scope.Resolve<TcpRequestListener>();
                    StartRequestListener(requestListener);
                }
        }

        private static void StartJobRunner(JobRunner jobRunner)
        {
            Task.Factory.StartNew(async () =>
            {
                _logger.LogInformation("Starting JobRunner...");
                while (true)
                {
                    await Task.Delay(2000);
                    try
                    {
                        await jobRunner.RunAsync();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogCritical(ex, "Ops. Something went wrong with JobRunner");
                        await Task.Delay(60_000);
                    }
                }
            });
        }


        private static void StartRequestListener(TcpRequestListener requestListener)
        {
            _logger.LogInformation("Starting TcpRequestListener...");
            requestListener.Start();
            //while (true)
            {
                try
                {
                    requestListener.Listen();
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex, "Ops. Something went wrong with TcpRequestListener.");
                }
            }
        }

        private static IContainer GetContainer()
        {
            var services = new ServiceCollection();

            services.AddSingleton(new LoggerFactory()
                .AddConsole(LogLevel.Trace)
                .AddDebug());

            services.AddLogging();

            var builder = new ContainerBuilder();

            builder.Register(
                c =>
                {
                    var optionsBuilder = new DbContextOptionsBuilder<VendingMachineDbContext>();
                    optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database=VendingMachine;trusted_connection=true;Integrated Security=True");
                    var dbContext = new VendingMachineDbContext(optionsBuilder.Options);
                    dbContext.Database.EnsureCreated();
                    return dbContext;
                })
                .As<VendingMachineDbContext>()
                .As<DbContext>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(ClientCardRepository).Assembly)
                   .Where(t => t.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(SaleService).Assembly)
                   .Where(t => t.Name.EndsWith("Service"))
                   .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(RemoteMachineService).Assembly)
                   .Where(t => t.Name.EndsWith("Service"))
                   .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(ActionHandlerProvider).Assembly)
                   .Where(t => t.Name.EndsWith("Provider"))
                   .AsImplementedInterfaces();
            
            builder.RegisterAssemblyTypes(typeof(ActionHandlerProvider).Assembly)
                   .Where(t => t.Name.EndsWith("ActionHandler"))
                   .AsSelf();

            builder.RegisterAssemblyTypes(typeof(ActionHandlerProvider).Assembly)
                   .Where(t => t.Name.EndsWith("ActionHandler"))
                   .AsSelf();

            builder.RegisterType<TcpRequestListener>();
            builder.RegisterType<JobRunner>();

            builder.Populate(services);

            return builder.Build();
        }
    }
}
