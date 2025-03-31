using System.Collections.Generic;
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
        
        Task<GetSystemMenuListResponse> GetSystemMenuListAsync(GetSystemMenuListInputDto input, CancellationToken cancellationToken);

        Task<List<GetSystemMenuTreeResponse>> GetSystemMenuTreeResponseAsync(CancellationToken cancellationToken);

        /// <summary>
        /// 查询菜单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SystemMenuDto> GetSystemMenuAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> CreateSystemMenuAsync(CreateSystemMenuInputDto input, CancellationToken cancellationToken);

        /// <summary>
        /// 删除菜单内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteSystemMenuAsync(Guid id, CancellationToken cancellationToken);
    }


    public class SystemMenuAppService
        (ISysetemMenuRepository systemMenuRepository) : RYDesignAppService, ISystemMenuAppService
    {

        /// <summary>
        /// 菜单列表分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<GetSystemMenuListResponse> GetSystemMenuListAsync(GetSystemMenuListInputDto input, CancellationToken cancellationToken)
        {
            var queryable = (await systemMenuRepository.GetQueryableAsync())
                .AsNoTracking()
                .WhereIf(!string.IsNullOrEmpty(input.MenuName), x => x.MenuName.Contains(input.MenuName))
                .WhereIf(!string.IsNullOrEmpty(input.MenuPath), x => x.MenuPath.Contains(input.MenuPath));

            var entity = await queryable.ToListAsync(cancellationToken);

            var entityPart = entity.Where(x => x.ParentId == null).ToList();
            if (entity.Count > 0 && entityPart.Count > 0)
            {
                List<SystemMenuDto> systemMenuDtos = new List<SystemMenuDto>();
                foreach (var systemMenu in entityPart)
                {
                    systemMenuDtos.Add(SystemMenuSumMenusList(systemMenu));
                }
                int count = systemMenuDtos.Count;

                return new GetSystemMenuListResponse()
                {
                    Items = systemMenuDtos.Skip(input.SkipCount).Take(input.MaxResultCount).ToList(),
                    TotalCount = count
                };
            }
            return new GetSystemMenuListResponse()
            {
                Items = new List<SystemMenuDto>(),
                TotalCount = 0
            };
        }


        /// <summary>
        /// 返回树形菜单内容
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<GetSystemMenuTreeResponse>> GetSystemMenuTreeResponseAsync(CancellationToken cancellationToken)
        {
            var queryable =await (await systemMenuRepository.GetQueryableAsync())
                .AsNoTracking()
                .Include(x => x.SubMenus)
                .ThenInclude(x=> x.SubMenus)
                .OrderBy(x => x.OrderIndex)
                .ToListAsync();
            return GetSystemMenuTreeResponseDto(queryable);
        }

        private List<GetSystemMenuTreeResponse> GetSystemMenuTreeResponseDto(List<System_Menu> list)
        {
            List<GetSystemMenuTreeResponse> getSystemMenuTree = new List<GetSystemMenuTreeResponse>();
            foreach (var item in list)
            {
                getSystemMenuTree.Add(new GetSystemMenuTreeResponse()
                {
                    label = item.MenuName,
                    value = item.Id.ToString(),
                    children = GetSystemMenuTreeResponseDto(item.SubMenus.ToList())
                });
            }
            return getSystemMenuTree;
        }


        /// <summary>
        /// 查询单个菜单信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<SystemMenuDto> GetSystemMenuAsync(Guid id
            , CancellationToken cancellationToken)
        {
            var entity = await systemMenuRepository.GetIncludeAsync(x => x.Id == id, x => x.SubMenus,cancellationToken);
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
                MenuType = entity.MenuType,
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

        public async Task<bool> CreateSystemMenuAsync(CreateSystemMenuInputDto input, CancellationToken cancellationToken)
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
                input.MenuType,
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
        public async Task<bool> DeleteSystemMenuAsync(Guid id, CancellationToken cancellationToken)
        {
            return await systemMenuRepository.DeleteAsync(await systemMenuRepository.GetIncludeAsync(x => x.Id == id, x => x.SubMenus, cancellationToken));
        }

        
    }
}
