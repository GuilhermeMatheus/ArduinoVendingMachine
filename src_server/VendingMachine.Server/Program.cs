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
        static void Main(string[] args)
        {
            var contextProvider = new ActionContextProvider();

            var optionsBuilder = new DbContextOptionsBuilder<VendingMachineDbContext>();
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;Database=VendingMachine;trusted_connection=true");
            var dbContext = new VendingMachineDbContext(optionsBuilder.Options);

            var clientCardRepository = new ClientCardRepository(dbContext);
            var machineRepository = new MachineRepository(dbContext);
            var productRepository = new ProductRepository(dbContext);
            var transactionRepository = new TransactionRepository(dbContext);
            var saleService = new SaleService(clientCardRepository, machineRepository, productRepository, transactionRepository);
            var handlerProvider = new ActionHandlerProvider(saleService);
            var requestListener = new TcpRequestListener(contextProvider, handlerProvider);

            requestListener.Start();

            while (true)
            {
                try
                {
                    requestListener.Listen();
                }
                catch (ActionNotSupportedException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
