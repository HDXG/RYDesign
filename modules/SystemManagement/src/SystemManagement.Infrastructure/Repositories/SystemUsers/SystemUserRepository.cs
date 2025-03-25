using SystemManagement.Domain.Repositories;
using SystemManagement.Domain.SystemUsers;
using SystemManagement.Infrastructure.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace SystemManagement.Infrastructure.Repositories.SystemUsers
{
    public interface ISystemUserRepository: ISystemManagmentRepository<System_User, Guid>
    {

    }

    public class SystemUserRepository(IDbContextProvider<SystemManagementDbContext> dbContextProvider) : SystemManagmentRepository<System_User, Guid>(dbContextProvider),ISystemUserRepository
    {
        
    }
}
