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
using VendingMachine.Core.Repository;
using VendingMachine.Core.Services;
using VendingMachine.EF;
using VendingMachine.EF.Repository;
using VendingMachine.Server.Actions;
using VendingMachine.Server.Exceptions;
using VendingMachine.Server.Request;

[assembly: InternalsVisibleTo("VendingMachine.Server.Tests")]
namespace VendingMachine.Server
{
    class Program
    {
        private static ILogger<Program> _logger;

        private static void Main(string[] args)
        {
            Console.WriteLine("Initializing server. . .");
            var container = GetContainer();
            using (var scope = container.BeginLifetimeScope())
            {
                var requestListener = scope.Resolve<TcpRequestListener>();
                _logger = scope.Resolve<ILogger<Program>>();
                _logger.LogInformation("Starting");

                StartRequestListener(requestListener);
            }
        }

        private static void StartRequestListener(IRequestListener requestListener)
        {
            requestListener.Start();

            while (true)
                try
                {
                    requestListener.Listen();
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex, "Ops. Something went wrong.");
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
                .As<DbContext>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(ClientCardRepository).Assembly)
                   .Where(t => t.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(SaleService).Assembly)
                   .Where(t => t.Name.EndsWith("Service"))
                   .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(ActionHandlerProvider).Assembly)
                   .Where(t => t.Name.EndsWith("Provider"))
                   .AsImplementedInterfaces();
            
            builder.RegisterAssemblyTypes(typeof(ActionHandlerProvider).Assembly)
                   .Where(t => t.Name.EndsWith("ActionHandler"))
                   .AsSelf();

            builder.RegisterType<TcpRequestListener>();

            builder.Populate(services);

            return builder.Build();
        }
    }
}
