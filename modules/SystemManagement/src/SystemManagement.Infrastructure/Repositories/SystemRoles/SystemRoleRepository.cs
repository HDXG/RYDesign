using Design.EntityFrameworkCore.Repositories;
using RYDesign.Domain.Repositories;
using SystemManagement.Domain.SystemRoles;
using SystemManagement.Infrastructure.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace SystemManagement.Infrastructure.Repositories.SystemRoles
{
    public interface ISystemRoleRepository : IRYDesignRepository<System_Role,Guid>
    {
    }
    public class SystemRoleRepository(IDbContextProvider<SystemManagementDbContext>dbContextProvider)
        :RYDesignEfCoreRepository<SystemManagementDbContext,System_Role,Guid>(dbContextProvider), ISystemRoleRepository
    {

    }
}
