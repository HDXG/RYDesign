using RYDesign.AppService;
using SystemManagement.Domain;
using Volo.Abp.Modularity;

namespace SystemManagement.AppService;


[DependsOn(
    typeof(RYDesignAppServiceModule),
    typeof(SystemManagemementDomainModule)
    )]
public class SystemManagementAppServiceModule:AbpModule
{

}


