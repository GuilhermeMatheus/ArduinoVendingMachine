using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Core.Repository;
using VendingMachine.EF.Extensions;

namespace VendingMachine.EF.Repository
{
    public abstract class EFRepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey>
         where TEntity : class
    {
        private readonly DbContext _dbContext;
        private readonly Expression<Func<TEntity, TKey>> _keySelector;

        public IQueryable<TEntity> Query => GetSet().AsQueryable();

        public EFRepositoryBase(DbContext dbContext, Expression<Func<TEntity, TKey>> keySelector)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _keySelector = keySelector ?? throw new ArgumentNullException(nameof(keySelector));
        }

        public TEntity Get(TKey key) =>
            GetSet().Find(key);

        public Task<TEntity> GetAsync(TKey key) =>
            GetSet().FindAsync(key);

        public IEnumerable<TEntity> GetMany(IEnumerable<TKey> keys)
        {
            var predicate = LinqExpressions.BuildContainsExpression(_keySelector, keys);
            return Query.Where(predicate).AsEnumerable();
        }

        public void Add(TEntity entity) =>
            GetSet().Add(entity);

        public void Delete(TEntity entity) =>
            GetSet().Remove(entity);

        public void Update(TEntity entity) =>
            GetSet().Update(entity);

        public List<TEntity> FetchAll() =>
            GetSet().ToList();

        public Task<List<TEntity>> FetchAllAsync() =>
            GetSet().ToListAsync();

        public int Save() =>
            _dbContext.SaveChanges();

        public Task<int> SaveAsync() =>
            _dbContext.SaveChangesAsync();

        private DbSet<TEntity> GetSet() => 
            _dbContext.Set<TEntity>();
    }
}
