using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace RYDesign.AspNetCore;

[DependsOn(
         typeof(AbpAspNetCoreMvcModule)
        )]
public class RYDesignAspNetCoreModule : AbpModule
{

}