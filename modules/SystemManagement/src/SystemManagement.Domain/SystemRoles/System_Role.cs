using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace SystemManagement.Domain.SystemRoles
{
    public class System_Role:Entity<Guid>
    {
        public System_Role()
        {

        }

        public System_Role(Guid Id,string roleName,string describe,int orderIndex,bool isDefault) :base(Id)
        {
            SetStatusToTrue();
            ChangeRoleName(roleName);
            ChangeDescribe(describe);
            OrderIndex = orderIndex;
            IsDefault = isDefault;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
        public void ChangeRoleName(string roleName)
        {
            RoleName = Check.NotNullOrWhiteSpace(roleName, "RoleName");
        }

        /// <summary>
        /// 角色说明
        /// </summary>
        public string Describe { get; set; }
        public void ChangeDescribe(string describe)
        {
            Describe = Check.NotNullOrWhiteSpace(describe, "Describe");
        }

        

        /// <summary>
        /// 是否默认角色
        /// </summary>

        public bool IsDefault { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int OrderIndex { get; set; }


        

        /// <summary>
        /// 状态  启用/禁用
        /// </summary>
        public bool IsStatus { get; private set; }

        public void SetStatusToTrue()
        {
            IsStatus = true;
        }

        public void SetStatusToFalse()
        {
            IsStatus = false;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }



        public ICollection<System_RoleMenu> System_RoleMenus { get; set; } = new List<System_RoleMenu>();

        public void AddSystem_RoleMenu(System_RoleMenu system_RoleMenu)
        {
            System_RoleMenus.Add(system_RoleMenu);
        }

        public void RemoveSystem_RoleMenu(System_RoleMenu system_RoleMenu)
        {
            System_RoleMenus.Remove(system_RoleMenu);
        }

        public void ClearSystem_RoleMenu()
        {
            System_RoleMenus.Clear();
        }


    }
}
