using RYDesign.Application.Contracts.Dtos;

namespace SystemManagement.AppService.SystemUsers.Dtos
{
    public class SystemUserDto : EntityDto<Guid>
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        
        public SystemUserRoleDto[] Roles { get; set; }
    }

    public record SystemUserRoleDto(Guid roleId ,string RoleName);
}
