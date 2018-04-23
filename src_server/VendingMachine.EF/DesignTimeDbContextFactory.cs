using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.EF
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<VendingMachineDbContext>
    {
        public VendingMachineDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<VendingMachineDbContext>();
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database=VendingMachine;trusted_connection=true;Integrated Security=True");
            var dbContext = new VendingMachineDbContext(optionsBuilder.Options);
            dbContext.Database.EnsureCreated();
            return dbContext;
        }
    }
}
