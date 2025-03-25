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
            System_User system_User = new System_User(GuidGenerator.Create(), input.AccountNumber, input.PassWord, input.UserName, true);

            await systemUserRepository.InsertAsync(system_User);
            return true;

        }
    }
}
