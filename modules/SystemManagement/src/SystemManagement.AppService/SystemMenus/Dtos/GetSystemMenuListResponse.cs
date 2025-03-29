using RYDesign.Application.Contracts.Dtos;

namespace SystemManagement.AppService.SystemMenus.Dtos
{
    
    public class GetSystemMenuListInputDto : RYPagedRequestDto
    {
        public string MenuName { get; set; }

        public string MenuPath { get; set; }
    }

    public class GetSystemMenuListResponse : RYPagedResultDto<SystemMenuDto>;

}
