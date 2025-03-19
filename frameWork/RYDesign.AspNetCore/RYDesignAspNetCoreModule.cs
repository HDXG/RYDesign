using Volo.Abp.AspNetCore;
using Volo.Abp.Modularity;

namespace RYDesign.AspNetCore;

[DependsOn(
         typeof(AbpAspNetCoreModule)
        )]
public class RYDesignAspNetCoreModule : AbpModule
{

}