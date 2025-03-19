using RYDesign.Domain;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace RYDesign.EntityFrameworkCore;

[DependsOn(
    typeof(RYDesignDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
    )]
public class RYDesignEntityFrameworkCoreModule:AbpModule
{

}
