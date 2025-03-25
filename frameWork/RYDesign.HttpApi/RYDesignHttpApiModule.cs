using RYDesign.AspNetCore;
using Volo.Abp.Modularity;

namespace RYDesign.HttpApi
{
    [DependsOn(
        typeof(RYDesignAspNetCoreModule)
        )]
    public class RYDesignHttpApiModule:AbpModule
    {

    }
}
