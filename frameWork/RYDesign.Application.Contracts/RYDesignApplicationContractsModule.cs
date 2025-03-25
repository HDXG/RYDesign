using Volo.Abp.Application;
using Volo.Abp.FluentValidation;
using Volo.Abp.Modularity;

namespace RYDesign.Application.Contracts
{
    [DependsOn(
            typeof(AbpDddApplicationContractsModule),
            typeof(AbpFluentValidationModule)
        )]
    public class RYDesignApplicationContractsModule:AbpModule
    {

    }
}
