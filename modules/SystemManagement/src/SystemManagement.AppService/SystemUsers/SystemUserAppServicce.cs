using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using RYDesign.Application.Contracts.Service;
using RYDesign.Application.Service;
using SystemManagement.AppService.SystemUsers.Dtos;
using SystemManagement.Domain.SystemUsers;
using SystemManagement.Infrastructure.Repositories.SystemUsers;

namespace SystemManagement.AppService.SystemUsers
{
    public interface ISystemUserAppServicce:IRYDesignAppService
    {

        Task<GetSystemUserPagedListResponse> GetSystemUserPagedListAsync(GetSystemUserPagedListInputDto input, CancellationToken cancellationToken);

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> CreateSystemUserAsync(CreateSystemUserInputDto input, CancellationToken cancellationToken);

        Task<GetSystemUserResponse> GetSystemUserAsync(Guid id, CancellationToken cancellationToken);

    }


    public class SystemUserAppServicce(ISystemUserRepository systemUserRepository)
        : RYDesignAppService, ISystemUserAppServicce
    {
        /// <summary>
        /// 创建用户用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> CreateSystemUserAsync(CreateSystemUserInputDto input, CancellationToken cancellationToken)
        {
            Guid UserId = GuidGenerator.Create();
            System_User system_User = new System_User(UserId, input.AccountNumber, input.PassWord, input.UserName, true);

            foreach (var item in input.CreateUserRoles)
            {
                system_User.AddRole(new System_UserRole(GuidGenerator.Create(), UserId, item.RoleId, item.RoleName));
            }

            await systemUserRepository.InsertAsync(system_User,false, cancellationToken);
            return true;

        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<GetSystemUserResponse> GetSystemUserAsync(Guid id, CancellationToken cancellationToken)
        {
            var querable = await (await systemUserRepository.GetQueryableAsync())
                .Include(x=>x.system_UserRoles)
                .AsNoTracking()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
            if(querable == null)
            {
                return new GetSystemUserResponse();
            }

            return new GetSystemUserResponse()
            {
                AccountNumber = querable.AccountNumber,
                UserName = querable.UserName,
                Roles = querable.system_UserRoles.Select(x => new SystemUserRoleDto(x.RoleId,x.RoleName)).ToArray(),
                Id = querable.Id
            };
        }


        /// <summary>
        /// 获取用户分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<GetSystemUserPagedListResponse> GetSystemUserPagedListAsync(GetSystemUserPagedListInputDto input, CancellationToken cancellationToken)
        {
            var queryable = (await systemUserRepository.GetQueryableAsync())
                .Include(x => x.system_UserRoles)
                .AsNoTracking()
                .WhereIf(!string.IsNullOrEmpty(input.UserName), x => x.UserName.Contains(input.UserName))
                .WhereIf(!string.IsNullOrEmpty(input.RoleName),x=>x.system_UserRoles.Any(a=>a.RoleName.Contains(input.RoleName)));

            var count = await queryable.LongCountAsync(cancellationToken);

            var items = await queryable
                .OrderBy(x=>x.Id)
                .PageBy(input.SkipCount, input.MaxResultCount)
                .Select(x => new GetSystemUserPagedListDto()
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    AccountNumber = x.AccountNumber,
                    IsStatus = x.IsStatus,
                    RoleName = string.Join(',', x.system_UserRoles.Select(x => x.RoleName).ToArray())
                })
                .ToListAsync(cancellationToken);
            return new GetSystemUserPagedListResponse
            {
                TotalCount = count,
                Items = items
            };
        }
    }
}
