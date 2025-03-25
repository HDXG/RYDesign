using Volo.Abp.Application.Dtos;

namespace RYDesign.Application.Contracts.Dtos
{
    public class RYPagedRequestDto: PagedResultRequestDto
    {

        private int _pageIndex = 1;

        private int _pageSize = 10;

        public virtual int PageIndex
        {
            get => _pageIndex;
            set => _pageIndex = value <= 0 ? 1 : value;
        }

        public virtual int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > 10000 ? 10 : value <= 0 ? 10 : value;
        }

        public override int MaxResultCount => PageSize;

        public override int SkipCount => (PageIndex - 1) * PageSize;
        
    }
}
