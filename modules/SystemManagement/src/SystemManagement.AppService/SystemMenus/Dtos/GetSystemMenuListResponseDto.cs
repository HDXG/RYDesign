using RYDesign.Application.Contracts.Dtos;

namespace SystemManagement.AppService.SystemMenus.Dtos
{
    public class GetSystemMenuListResponseDto: RYPagedResultDto<SystemMenuDto>
    {
        public GetSystemMenuListResponseDto()
        {

        }

        public GetSystemMenuListResponseDto(long totalCount, IReadOnlyList<SystemMenuDto> items)
            : base(totalCount, items)
        {
            Items = items;
            TotalCount = totalCount;
        }
    }

   
}
