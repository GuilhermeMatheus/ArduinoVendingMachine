using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Core.Model;

namespace VendingMachine.EF
{
    public class VendingMachineDbContext : DbContext
    {
        public VendingMachineDbContext(DbContextOptions options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<ClientCard>()
                .Property(i => i.Rfid)
                .ValueGeneratedNever();

            modelBuilder
                .Entity<Product>()
                .Property(i => i.Id)
                .ValueGeneratedNever();

            modelBuilder
                .Entity<ProductRail>()
                .HasKey(t => new { t.MachineId, t.ProductId });
        }

        public virtual DbSet<ClientCard> ClientCards { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Machine> Machines { get; set; }
        public virtual DbSet<ProductRail> ProductRails { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
    }
}
