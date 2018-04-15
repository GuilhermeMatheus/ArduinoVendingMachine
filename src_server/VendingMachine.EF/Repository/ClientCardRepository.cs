using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Core.Model;
using VendingMachine.Core.Repository;

namespace VendingMachine.EF.Repository
{
    public class ClientCardRepository : EFRepositoryBase<ClientCard, long>, IClientCardRepository
    {
        public ClientCardRepository(DbContext dbContext) 
            : base(dbContext, _ => _.Rfid)
        {
        }
    }
}
