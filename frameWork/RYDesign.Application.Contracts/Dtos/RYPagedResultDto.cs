using Volo.Abp.Application.Dtos;

namespace RYDesign.Application.Contracts.Dtos
{
    public class RYPagedResultDto<TDto> : PagedResultDto<TDto>
    {
        public RYPagedResultDto()
        {
        }


        public RYPagedResultDto(long totalCount, IReadOnlyList<TDto> items)
            : base(totalCount, items)
        {
           
        }
    }
}
