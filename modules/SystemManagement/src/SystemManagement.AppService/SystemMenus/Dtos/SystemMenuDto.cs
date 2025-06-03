using RYDesign.Application.Contracts.Dtos;

namespace SystemManagement.AppService.SystemMenus.Dtos
{
    public class SystemMenuDto: EntityDto<Guid>
    {
        /// <summary>
        /// 菜单/按钮 名称
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        ///  菜单路径
        /// </summary>
        public string MenuPath { get; set; }

        /// <summary>
        /// 菜单类型 0目录 1菜单 2按钮
        /// </summary>
        public int MenuType { get; set; }

        /// <summary>
        ///   图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        ///  权限标识
        /// </summary>
        public string PermissionKey { get; set; }

        /// <summary>
        ///   组件路径
        /// </summary>
        public string ComponentPath { get; set; }

        /// <summary>
        ///  路由名称
        /// </summary>
        public string RouteName { get; set; }

        /// <summary>
        ///  是否外链
        /// </summary>
        public bool ExternalLink { get; set; }

        /// <summary>
        /// 备注描述或者说明
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        ///  排序
        /// </summary>
        public int OrderIndex { get; set; }


        /// <summary>
        ///  菜单/按钮状态 启用/禁用
        /// </summary>
        public bool IsStatus { get; set; }

        public List<SystemMenuDto> children { get; set; } = new List<SystemMenuDto>();
    }
}
