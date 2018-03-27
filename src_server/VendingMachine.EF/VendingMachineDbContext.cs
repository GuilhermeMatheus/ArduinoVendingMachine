using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Core.Model;

namespace VendingMachine.EF
{
    public class VendingMachineDbContext : DbContext
    {
        public VendingMachineDbContext()
            : this("DefaultConnection")
        {
        }

        public VendingMachineDbContext(string nameOrConnectionString) 
            : base(nameOrConnectionString)
        {
        }

        public virtual DbSet<ClientCard> ClientCards { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Product> Machines { get; set; }
    }
}
