using Volo.Abp.FluentValidation;
using Volo.Abp.Guids;
using Volo.Abp.Modularity;

namespace RYDesign.AppService;

[DependsOn(
     typeof(AbpFluentValidationModule),
     typeof(AbpGuidsModule)
    )]
public class RYDesignAppServiceModule:AbpModule
{

}
