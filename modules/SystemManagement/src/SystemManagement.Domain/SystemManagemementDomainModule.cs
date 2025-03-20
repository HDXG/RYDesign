using RYDesign.Domain;
using Volo.Abp.Modularity;

namespace SystemManagement.Domain;

[DependsOn(
    typeof(RYDesignDomainModule)
    )]
public class SystemManagemementDomainModule:AbpModule
{

}
