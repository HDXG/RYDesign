using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;

namespace RYDesign.AppService;

public interface IRYDesignAppService : ITransientDependency;

public class RYDesignAppService : IRYDesignAppService
{
    public IAbpLazyServiceProvider lazyServiceProvider { get; set; }

    protected IGuidGenerator GuidGenerator => lazyServiceProvider.LazyGetRequiredService<IGuidGenerator>();
}
