using RYDesign.Application.Contracts;
using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace RYDesign.Application
{
    [DependsOn(
        typeof(AbpDddApplicationModule),
        typeof(RYDesignApplicationContractsModule)
        )]
    public class RYDesignApplicationModule:AbpModule
    {

    }
}
