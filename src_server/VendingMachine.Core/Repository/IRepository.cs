using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Core.Model;

namespace VendingMachine.Core.Repository
{
    public interface IClientCardRepository : IRepository<ClientCard, long> { }
    public interface IMachineRepository : IRepository<Machine, int> { }
    public interface IProductRepository : IRepository<Product, int> { }
    public interface ITransactionRepository : IRepository<Transaction, int> { }

    public interface IRepository<TEntity, TKey>
    {
        List<TEntity> FetchAll();
        IQueryable<TEntity> Query { get; }

        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Save();
    }
}
