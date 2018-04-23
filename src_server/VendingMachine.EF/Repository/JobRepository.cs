using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using VendingMachine.Core.Model;
using VendingMachine.Core.Repository;

namespace VendingMachine.EF.Repository
{
    public class JobRepository : EFRepositoryBase<Job, int>, IJobRepository
    {
        public JobRepository(DbContext dbContext)
            : base(dbContext, _ => _.Id)
        {
        }
    }
}
