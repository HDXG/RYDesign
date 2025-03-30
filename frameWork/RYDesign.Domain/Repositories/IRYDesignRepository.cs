using System.Linq.Expressions;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace RYDesign.Domain.Repositories;

public interface IRYDesignRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
{
    /// <summary>
    /// 删除单个对象
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(TEntity entity);
    
    Task<TEntity> GetIncludeAsync(
        Expression<Func<TEntity, bool>> wherePredicate,
        Expression<Func<TEntity, IEnumerable<TEntity>>> includePredicate,CancellationToken cancellationToken);

    Task<List<TEntity>> GetListIncludeAsync(
        Expression<Func<TEntity, bool>> wherePredicate,
        Expression<Func<TEntity, IEnumerable<TEntity>>> includePredicate, CancellationToken cancellationToken);


}