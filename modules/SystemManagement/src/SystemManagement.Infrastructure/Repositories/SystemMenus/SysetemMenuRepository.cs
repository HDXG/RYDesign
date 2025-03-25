using Design.EntityFrameworkCore.Repositories;
using RYDesign.Domain.Repositories;
using SystemManagement.Domain.SystemMenus;
using SystemManagement.Infrastructure.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace SystemManagement.Infrastructure.Repositories.SystemMenus
{
    public interface ISysetemMenuRepository : IRYDesignRepository<System_Menu, Guid>
    {

    }

    public class SysetemMenuRepository(IDbContextProvider<SystemManagementDbContext> dbContextProvider):RYDesignEfCoreRepository<SystemManagementDbContext, System_Menu, Guid>(dbContextProvider), ISysetemMenuRepository
    {

    }
}
