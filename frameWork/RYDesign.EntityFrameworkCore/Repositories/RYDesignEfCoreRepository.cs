using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using RYDesign.Domain.Repositories;
using RYDesign.EntityFrameworkCore.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
namespace Design.EntityFrameworkCore.Repositories
{
    public abstract class RYDesignEfCoreRepository<TDbContext, TEntity, TKey>(IDbContextProvider<TDbContext> dbContextProviders) : EfCoreRepository<TDbContext, TEntity, TKey>(dbContextProviders), IRYDesignRepository<TEntity, TKey> where TDbContext : IRYDesignEfCoreContext where TEntity : class, IEntity<TKey>
    {
        public async Task<TEntity> GetIncludeAsync(Expression<Func<TEntity, bool>> wherePredicate, Expression<Func<TEntity, IEnumerable<TEntity>>> includePredicate)
        {
            var db = await dbContextProviders.GetDbContextAsync();
            return await db.Set<TEntity>().Include(includePredicate).ThenInclude(includePredicate).FirstOrDefaultAsync(wherePredicate);
        }

        public async Task<List<TEntity>> GetListIncludeAsync(Expression<Func<TEntity, bool>> wherePredicate, Expression<Func<TEntity, IEnumerable<TEntity>>> includePredicate)
        {
            var db = await dbContextProviders.GetDbContextAsync();
            return await db.Set<TEntity>().Where(wherePredicate).Include(includePredicate).ThenInclude(includePredicate).ToListAsync();
        }

        public async Task<(int, List<TEntity>)> GetPagedListAsync<Key>(int skipCount, int taskCount, Expression<Func<TEntity, bool>> wherePredicate, Func<TEntity, Key> orderPredicate, bool isReverse = true)
        {
            List<TEntity> queryable = await (await GetQueryableAsync()).Where(wherePredicate).ToListAsync();
            var count = queryable.Count();
            var queryable2 = (isReverse ? queryable.OrderByDescending(orderPredicate) : queryable.OrderBy(orderPredicate));
            return (count, queryable2.Skip(skipCount).Take(taskCount).ToList());
        }

        public Task<(int, List<TEntity>)> GetPagedListAsync<Key>(Expression<Func<TEntity, bool>> wherePredicate, Func<TEntity, Key> orderPredicate, bool isReverse = true, int pageIndex = 1, int pageSize = 10)
        {
            return GetPagedListAsync((pageIndex - 1) * pageSize, pageSize, wherePredicate, orderPredicate, isReverse);
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            var db = await dbContextProviders.GetDbContextAsync();
            db.Set<TEntity>().Remove(entity);
            return await db.SaveChangesAsync() > 0;
        }

       
    }



}
