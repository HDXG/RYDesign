using RYDesign.Application.Contracts.Dtos;

namespace SystemManagement.AppService.SystemUsers.Dtos
{
    public class GetSystemUserPagedListResponse : RYPagedResultDto<GetSystemUserPagedListDto>;

    public class GetSystemUserPagedListDto:EntityDto<Guid>
    {

        public string UserName { get; set; }

        public string AccountNumber { get; set; }
        
        public bool IsStatus { get; set; }
        
        public string RoleName { get; set; }
    }


    /// <summary>
    /// 获取系统用户分页列表入参
    /// </summary>
    public class GetSystemUserPagedListInputDto : RYPagedRequestDto
    {
        public string UserName { get; set; }

        public string RoleName { get; set; }
    }
}
