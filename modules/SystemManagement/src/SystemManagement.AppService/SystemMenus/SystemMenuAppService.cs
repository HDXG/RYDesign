using Microsoft.EntityFrameworkCore;
using RYDesign.Application.Contracts.Service;
using RYDesign.Application.Service;
using SystemManagement.AppService.SystemMenus.Dtos;
using SystemManagement.Domain.SystemMenus;
using SystemManagement.Infrastructure.Repositories.SystemMenus;
using Volo.Abp.Domain.Repositories;

namespace SystemManagement.AppService.SystemMenus
{
    public interface ISystemMenuAppService : IRYDesignAppService
    {
        
        Task<GetSystemMenuListResponseDto> GetSystemMenuListAsync(GetSystemMenuListInputDto input);

        /// <summary>
        /// 查询菜单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SystemMenuDto> GetSystemMenuAsync(Guid id);

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> CreateSystemMenuAsync(CreateSystemMenuInputDto input);

        /// <summary>
        /// 删除菜单内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteSystemMenuAsync(Guid id);
    }


    public class SystemMenuAppService
        (ISysetemMenuRepository systemMenuRepository) : RYDesignAppService, ISystemMenuAppService
    {


        public async Task<GetSystemMenuListResponseDto> GetSystemMenuListAsync(GetSystemMenuListInputDto input)
        {
            var queryable = (await systemMenuRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrEmpty(input.MenuName), x => x.MenuName.Contains(input.MenuName))
                .WhereIf(!string.IsNullOrEmpty(input.MenuPath), x => x.MenuPath.Contains(input.MenuPath));

            var entity = await queryable.ToListAsync();

            var entityPart = entity.Where(x => x.ParentId == null).ToList();
            if (entity.Count > 0 && entityPart.Count > 0)
            {
                List<SystemMenuDto> systemMenuDtos = new List<SystemMenuDto>();
                foreach (var systemMenu in entityPart)
                {
                    systemMenuDtos.Add(SystemMenuSumMenusList(systemMenu));
                }
                int count = systemMenuDtos.Count;

                return new GetSystemMenuListResponseDto()
                {
                    Items = systemMenuDtos.Skip(input.SkipCount).Take(input.MaxResultCount).ToList(),
                    TotalCount = count
                };
            }
            return new GetSystemMenuListResponseDto(0,new List<SystemMenuDto>());
        }

        public async Task<SystemMenuDto> GetSystemMenuAsync(Guid id)
        {
            var entity = await systemMenuRepository.GetIncludeAsync(x => x.Id == id, x => x.SubMenus);
            if (entity != null)
            {
                SystemMenuDto sysMenuDto = SystemMenuSumMenusList(entity);
                return sysMenuDto;
            }
            else
            {
                return new SystemMenuDto();
            }
        }

        private SystemMenuDto SystemMenuSumMenusList(System_Menu entity)
        {
            var sysMenuDto = new SystemMenuDto()
            {
                Id = entity.Id,
                MenuName = entity.MenuName,
                ParentId = entity.ParentId,
                MenuPath = entity.MenuPath,
                Icon = entity.Icon,
                PermissionKey = entity.PermissionKey,
                ComponentPath = entity.ComponentPath,
                RouteName = entity.RouteName,
                ExternalLink = entity.ExternalLink,
                Remark = entity.Remark,
                OrderIndex = entity.OrderIndex,
                IsStatus = entity.IsStatus
            };
            if (entity.SubMenus.Count > 0)
            {
                List<SystemMenuDto> systemMenuDtos = new List<SystemMenuDto>();
                foreach (var item in entity.SubMenus)
                {
                    systemMenuDtos.Add(SystemMenuSumMenusList(item));
                }
                sysMenuDto.SubMenus = systemMenuDtos;
            }
            return sysMenuDto;
        }

        public async Task<bool> CreateSystemMenuAsync(CreateSystemMenuInputDto input)
        {
            var systemMenu = CreateSystemMenu(input, null);
            await systemMenuRepository.InsertAsync(systemMenu);
            return true;
        }

        private System_Menu CreateSystemMenu(CreateSystemMenuInputDto input, System_Menu? parent)
        {
            var systemMenu = new System_Menu(
                GuidGenerator.Create(),
                input.MenuName,
                input.MenuPath,
                input.Icon,
                input.PermissionKey,
                input.RouteName,
                input.ComponentPath,
                input.OrderIndex,
                input.Remark
            );

            if (parent != null)
            {
                systemMenu.ChangeParentMenuId(parent.Id);
            }

            if (input.Childrens.Length > 0)
            {
                foreach (var child in input.Childrens)
                {
                    var subSystemMenu = CreateSystemMenu(child, systemMenu);
                    systemMenu.AddSubMenu(subSystemMenu);
                }
            }

            return systemMenu;
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteSystemMenuAsync(Guid id)
        {
            return await systemMenuRepository.DeleteAsync(await systemMenuRepository.GetIncludeAsync(x => x.Id == id, x => x.SubMenus));
        }

        
    }
}
