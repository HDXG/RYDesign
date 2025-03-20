using Design.EntityFrameworkCore.Repositories;
using SystemManagement.Domain.Repositories;
using SystemManagement.Infrastructure.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;
using Volo.Abp.EntityFrameworkCore;

namespace SystemManagement.Infrastructure.Repositories
{
    public class SystemManagmentRepository<TEntity, TKey>
        (IDbContextProvider<SystemManagementDbContext> dbContextProvider) :
        RYDesignEfCoreRepository<SystemManagementDbContext, TEntity, TKey>(dbContextProvider),
        ISystemManagmentRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        
    }
}
