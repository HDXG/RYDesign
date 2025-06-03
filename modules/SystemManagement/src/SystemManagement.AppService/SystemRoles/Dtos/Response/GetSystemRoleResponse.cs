using Volo.Abp.Application.Dtos;

namespace SystemManagement.AppService.SystemRoles.Dtos.Response
{
    public class GetSystemRoleResponse:EntityDto<Guid>
    {
        public string RoleName { get; set; }

        public string Describe { get; set; }

        public int OrderIndex { get; set; }

        public bool IsDefault { get; set; }

        public bool IsStatus { get; set; }

    }
}
