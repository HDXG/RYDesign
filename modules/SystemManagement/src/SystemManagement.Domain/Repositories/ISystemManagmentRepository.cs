using RYDesign.Domain.Repositories;
using Volo.Abp.Domain.Entities;

namespace SystemManagement.Domain.Repositories;

public interface ISystemManagmentRepository<TEntity, TKey> : IRYDesignRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
{

}
