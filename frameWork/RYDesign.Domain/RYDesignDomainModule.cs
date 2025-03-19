using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace RYDesign.Domain;

[DependsOn(
    typeof(AbpDddDomainModule)
    )]
public class RYDesignDomainModule:AbpModule
{

}
