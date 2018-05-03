using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Core.Model;
using VendingMachine.Core.Repository;

namespace VendingMachine.EF.Repository
{
    public class ProductRepository : EFRepositoryBase<Product, int>, IProductRepository
    {
        public ProductRepository(DbContext dbContext)
            : base(dbContext, _ => _.Id)
        {
        }

        public IEnumerable<ProductRail> GetProductsInMachine(Machine machine)
        {
            var machineId = machine.Id;
            var products = dbContext
                            .Set<ProductRail>()
                            .AsQueryable()
                            .Include(pr => pr.Product)
                            .Where(_ => _.MachineId == machineId);

            return products;
        }
    }
}
