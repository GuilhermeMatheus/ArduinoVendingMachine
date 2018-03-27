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
    public class MachineRepository : EFRepositoryBase<Machine, int>, IMachineRepository
    {
        public MachineRepository(DbContext dbContext) 
            : base(dbContext, _ => _.Id)
        {
        }
    }
}
