using Microsoft.Extensions.DependencyInjection;
using RYDesign.HttpApi;
using SystemManagement.AppService;
using SystemManagement.Infrastructure;
using Volo.Abp.Modularity;

namespace SystemManagement.HttpApi
{

    [DependsOn(
        typeof(SystemManagementAppServiceModule),
        typeof(SystemManagementInfrastructureModule),
        typeof(RYDesignHttpApiModule)
        
        )]
    public class SystemManagementHttpApiModule: AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(SystemManagementHttpApiModule).Assembly);
            });
            
        }
    }
}
