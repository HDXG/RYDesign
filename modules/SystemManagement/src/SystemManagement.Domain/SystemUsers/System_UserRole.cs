using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace SystemManagement.Domain.SystemUsers
{
    public class System_UserRole:Entity<Guid>
    {
        protected System_UserRole()
        {

        }

        public System_UserRole(Guid Id,Guid userId, Guid roleId, string roleName):base(Id)
        {
            UserId = userId;
            RoleId = roleId;
            ChangeRoleName(roleName);
            IsEnable = true;
        }

        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { get; private set; }
       

        /// <summary>
        /// 角色Id
        /// </summary>
        public Guid RoleId { get; private set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; private set; }
        public void ChangeRoleName(string roleName)
        {
            RoleName = Check.NotNullOrWhiteSpace(roleName, "RoleName");
        }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; private set; }

        /// <summary>
        /// 启用
        /// </summary>
        public void Enable()
        {
            IsEnable = true;
        }

        /// <summary>
        /// 禁用
        /// </summary>
        public void Forbidden()
        {
            IsEnable = false;
        }
    }
}
