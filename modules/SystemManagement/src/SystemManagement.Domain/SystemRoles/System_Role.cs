using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace SystemManagement.Domain.SystemRoles
{
    public class System_Role:Entity<Guid>
    {
        public System_Role()
        {

        }

        public System_Role(Guid Id,string roleName,string describe) :base(Id)
        {
            SetStatusToTrue();
            ChangeRoleName(roleName);
            ChangeDescribe(describe);
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


    }
}
