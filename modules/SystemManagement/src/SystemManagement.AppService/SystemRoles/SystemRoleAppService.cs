using Microsoft.EntityFrameworkCore;
using RYDesign.Application.Contracts.Service;
using RYDesign.Application.Service;
using SystemManagement.AppService.SystemRoles.Dtos;
using SystemManagement.Domain.SystemRoles;
using SystemManagement.Infrastructure.Repositories.SystemRoles;

namespace SystemManagement.AppService.SystemRoles
{
    public interface ISystemRoleAppService:IRYDesignAppService
    {

        Task<GetSystemRolePagedListResponse> GetSystemRolePagedListAsync(GetSystemRolePagedListRequest input);

        Task<bool> CreateSysteRoleAsync(CreateSystemRoleRequest input);
    }

    public class SystemRoleAppService(ISystemRoleRepository systemRoleRepository) : RYDesignAppService, ISystemRoleAppService
    {
        /// <summary>
        /// 创建系统角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> CreateSysteRoleAsync(CreateSystemRoleRequest input)
        {
           var entity = new System_Role(GuidGenerator.Create(), input.RoleName, input.Describe,input.OrderIndex,input.IsDefault);
            await systemRoleRepository.InsertAsync(entity);
            return true;
        }

        /// <summary>
        /// 获取系统角色分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetSystemRolePagedListResponse> GetSystemRolePagedListAsync(GetSystemRolePagedListRequest input)
        {
            var quable = (await systemRoleRepository.GetQueryableAsync())
                 .AsNoTracking()
                 .WhereIf(!string.IsNullOrEmpty(input.RoleName), x => x.RoleName.Contains(input.RoleName));
            var count = await quable.CountAsync();
            var list = await quable
                .OrderBy(x => x.OrderIndex)
                .PageBy(input.SkipCount,input.MaxResultCount)
                .Select(x=>new CreateSystemRoleRequestDto(x.Id,x.RoleName,x.Describe,x.OrderIndex,x.IsDefault,x.IsStatus,x.CreateTime))
                .ToListAsync();
            return new GetSystemRolePagedListResponse()
            {
                TotalCount = count,
                Items = list
            };

        }
    }
}
