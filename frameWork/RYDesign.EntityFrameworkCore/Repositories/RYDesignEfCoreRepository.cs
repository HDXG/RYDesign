using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;
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
        public async Task<TEntity> GetIncludeAsync(Expression<Func<TEntity, bool>> wherePredicate, Expression<Func<TEntity, IEnumerable<TEntity>>> includePredicate, CancellationToken cancellationToken)
        {
            var db = await dbContextProviders.GetDbContextAsync();
            return await db.Set<TEntity>().Include(includePredicate).ThenInclude(includePredicate).FirstOrDefaultAsync(wherePredicate, cancellationToken);
        }

        public async Task<List<TEntity>> GetListIncludeAsync(Expression<Func<TEntity, bool>> wherePredicate, Expression<Func<TEntity, IEnumerable<TEntity>>> includePredicate, CancellationToken cancellationToken)
        {
            var db = await dbContextProviders.GetDbContextAsync();
            return await db.Set<TEntity>().Where(wherePredicate).Include(includePredicate).ThenInclude(includePredicate).ToListAsync(cancellationToken);
        }


        public async Task<bool> DeleteAsync(TEntity entity)
        {
            var db = await dbContextProviders.GetDbContextAsync();
            db.Set<TEntity>().Remove(entity);
            return await db.SaveChangesAsync() > 0;
        }

       
    }



}
