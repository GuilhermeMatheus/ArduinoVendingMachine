using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    }
}
