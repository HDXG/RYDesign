using Microsoft.EntityFrameworkCore;
using RYDesign.Application.Contracts.Service;
using RYDesign.Application.Service;
using SystemManagement.AppService.SystemRoles.Dtos.Request;
using SystemManagement.AppService.SystemRoles.Dtos.Response;
using SystemManagement.Domain.SystemRoles;
using SystemManagement.Infrastructure.Repositories.SystemRoles;

namespace SystemManagement.AppService.SystemRoles
{
    public interface ISystemRoleAppService:IRYDesignAppService
    {

        Task<GetSystemRolePagedListResponse> GetSystemRolePagedListAsync(GetSystemRolePagedListRequest input, CancellationToken cancellationToken);

        Task<bool> CreateSysteRoleAsync(CreateSystemRoleRequest input, CancellationToken cancellationToken);

        Task<GetSystemRoleResponse> GetSystemRoleAsync(Guid Id);

        Task<List<GetRoleIdInMenuListResponse>> GetRoleIdInMenuListAsync(GetRoleIdInMenuListRequest req);

        //Task<bool> UpdateSystemRoleAsync(UpdateSystemRoleRequest input, CancellationToken cancellationToken);
    }

    public class SystemRoleAppService(ISystemRoleRepository systemRoleRepository,
        ISystemRoleMenuRepository systemRoleMenuRepository) : RYDesignAppService, ISystemRoleAppService
    {

        /// <summary>
        /// 创建系统角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> CreateSysteRoleAsync(CreateSystemRoleRequest input, CancellationToken cancellationToken)
        {
           var entity = new System_Role(GuidGenerator.Create(), input.RoleName, input.Describe,input.OrderIndex,input.IsDefault);

            foreach (var item in input.childer)
            {
                entity.AddSystem_RoleMenu(new System_RoleMenu(GuidGenerator.Create(),entity.Id,item.value,item.lable));
            }

            await systemRoleRepository.InsertAsync(entity,false,cancellationToken);
            return true;
        }

        public async Task<List<GetRoleIdInMenuListResponse>> GetRoleIdInMenuListAsync(GetRoleIdInMenuListRequest req)
        {
            var list = await (await systemRoleMenuRepository.GetQueryableAsync())
                .AsNoTracking()
                .Where(x =>x.RoleId == req.RoleId)
                .Select(x => new GetRoleIdInMenuListResponse(x.MenuId, x.MenuName))
                .ToListAsync();
            return list;
        }



        /// <summary>
        /// 获取系统角色分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetSystemRolePagedListResponse> GetSystemRolePagedListAsync(GetSystemRolePagedListRequest input, CancellationToken cancellationToken)
        {
            var quable = (await systemRoleRepository.GetQueryableAsync())
                 .AsNoTracking()
                 .WhereIf(!string.IsNullOrEmpty(input.RoleName), x => x.RoleName.Contains(input.RoleName));
            var count = await quable.CountAsync(cancellationToken);
            var list = await quable
                .OrderBy(x => x.OrderIndex)
                .PageBy(input.SkipCount,input.MaxResultCount)
                .Select(x=>new CreateSystemRoleRequestDto(x.Id,x.RoleName,x.Describe,x.OrderIndex,x.IsDefault,x.IsStatus,x.CreateTime))
                .ToListAsync(cancellationToken);
            return new GetSystemRolePagedListResponse()
            {
                TotalCount = count,
                Items = list
            };

        }


        /// <summary>
        /// 查询角色信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async  Task<GetSystemRoleResponse> GetSystemRoleAsync(Guid Id)
        {
            var entity = await systemRoleRepository.FindAsync(Id);
            return entity is null ?
                new GetSystemRoleResponse()
                :
                new GetSystemRoleResponse()
                {
                    Id = entity.Id,
                    RoleName = entity.RoleName,
                    Describe = entity.Describe,
                    OrderIndex = entity.OrderIndex,
                    IsDefault = entity.IsDefault,
                    IsStatus = entity.IsStatus
                };
        }
    }
}
