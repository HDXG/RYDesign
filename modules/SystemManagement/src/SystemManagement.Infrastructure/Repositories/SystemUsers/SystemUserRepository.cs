using SystemManagement.Domain.Repositories;
using SystemManagement.Domain.SystemUsers;
using SystemManagement.Infrastructure.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace SystemManagement.Infrastructure.Repositories.SystemUsers
{
    public interface ISystemUserRepository: ISystemManagmentRepository<system_User,Guid>
    {

    }

    public class SystemUserRepository(IDbContextProvider<SystemManagementDbContext> dbContextProvider) : SystemManagmentRepository<system_User, Guid>(dbContextProvider),ISystemUserRepository
    {
        
    }
}
