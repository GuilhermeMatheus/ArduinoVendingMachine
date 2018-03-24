using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Core.Model;
using Repository;
using VendingMachine.EF;
using Repository.EntityFramework;

namespace VendingMachine.DAL
{
    public class RepositoryFactory
    {
        private static VendingMachineDbContext GetDbContext()
            => new VendingMachineDbContext("DefaultConnection");

        public static Repository<Machine, int> GetMachineRepository()
            => new EFRepository<VendingMachineDbContext, Machine, int>(_ => _.Machines, GetDbContext());

        public static Repository<ClientCard, int> GetClientCardRepository()
            => new EFRepository<VendingMachineDbContext, ClientCard, int>(_ => _.ClientCards, GetDbContext());

        public static Repository<Product, int> GetProductRepository()
            => new EFRepository<VendingMachineDbContext, Product, int>(_ => _.Products, GetDbContext());

        public static Repository<Transaction, int> GetTransactionRepository()
            => new EFRepository<VendingMachineDbContext, Transaction, int>(_ => _.Transactions, GetDbContext());
    }
}
