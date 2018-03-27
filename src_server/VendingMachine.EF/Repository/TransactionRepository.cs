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
    public class TransactionRepository : EFRepositoryBase<Transaction, int>, ITransactionRepository
    {
        public TransactionRepository(DbContext dbContext) 
            : base(dbContext, _ => _.Id)
        {
        }
    }
}
