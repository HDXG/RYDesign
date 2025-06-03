using Design.EntityFrameworkCore.Repositories;
using RYDesign.Domain.Repositories;
using SystemManagement.Domain.SystemRoles;
using SystemManagement.Infrastructure.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace SystemManagement.Infrastructure.Repositories.SystemRoles
{
    public interface ISystemRoleMenuRepository: IRYDesignRepository<System_RoleMenu, Guid>
    {

    }


    public class SystemRoleMenuRepository(IDbContextProvider<SystemManagementDbContext> dbContextProvider):RYDesignRepository<SystemManagementDbContext,System_RoleMenu, Guid>(dbContextProvider),ISystemRoleMenuRepository
    {

    }
}
