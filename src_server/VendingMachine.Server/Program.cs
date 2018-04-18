using Autofac;
using Microsoft.EntityFrameworkCore;
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
        private static void Main(string[] args)
        {
            var container = GetContainer();
            using (var scope = container.BeginLifetimeScope())
            {
                var requestListener = scope.Resolve<TcpRequestListener>();
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
                catch (ActionNotSupportedException ex)
                {
                    Console.WriteLine(ex.Message);
                }
        }

        private static IContainer GetContainer()
        {
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

            return builder.Build();
        }
    }
}
