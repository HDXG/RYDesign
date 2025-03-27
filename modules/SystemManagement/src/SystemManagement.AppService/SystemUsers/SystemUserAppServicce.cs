using RYDesign.Application.Contracts.Service;
using RYDesign.Application.Service;
using SystemManagement.AppService.SystemUsers.Dtos;
using SystemManagement.Domain.SystemUsers;
using SystemManagement.Infrastructure.Repositories.SystemUsers;

namespace SystemManagement.AppService.SystemUsers
{
    public interface ISystemUserAppServicce:IRYDesignAppService
    {

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> CreateSystemUserAsync(CreateSystemUserInputDto input);
    }


    public class SystemUserAppServicce(ISystemUserRepository systemUserRepository)
        : RYDesignAppService, ISystemUserAppServicce
    {
        /// <summary>
        /// 创建用户用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> CreateSystemUserAsync(CreateSystemUserInputDto input)
        {
            Guid UserId = GuidGenerator.Create();
            System_User system_User = new System_User(UserId, input.AccountNumber, input.PassWord, input.UserName, true);

            foreach (var item in input.CreateUserRoles)
            {
                system_User.AddRole(new System_UserRole(GuidGenerator.Create(), UserId, item.RoleId, item.RoleName));
            }

            await systemUserRepository.InsertAsync(system_User);
            return true;

        }
    }
}
