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
        where TEntity : class
    {
        List<TEntity> FetchAll();
        IQueryable<TEntity> Query { get; }

        TEntity Get(TKey key);
        IEnumerable<TEntity> GetMany(IEnumerable<TKey> key);

        void Add(TEntity entity);
        void Delete(TEntity entity);
        int Save();
    }
}
