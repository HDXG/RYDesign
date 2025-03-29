using RYDesign.Application.Contracts.Dtos;

namespace SystemManagement.AppService.SystemRoles.Dtos
{
    public class GetSystemRolePagedListRequest:RYPagedRequestDto
    {
        public string RoleName { get; set; }
    }

    public class GetSystemRolePagedListResponse:RYPagedResultDto<CreateSystemRoleRequestDto>;


    public record CreateSystemRoleRequestDto(Guid Id,string RoleName, string Describe,int OrderIndex,
        bool IsDefault,bool IsStatus,DateTime CreateTime);
}
