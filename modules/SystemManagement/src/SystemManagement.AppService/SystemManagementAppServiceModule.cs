using RYDesign.Application;
using SystemManagement.Domain;
using Volo.Abp.Modularity;

namespace SystemManagement.AppService;


[DependsOn(
    typeof(RYDesignApplicationModule),
    typeof(SystemManagemementDomainModule)
    )]
public class SystemManagementAppServiceModule:AbpModule
{

}


