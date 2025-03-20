﻿using Microsoft.Extensions.DependencyInjection;
using RYDesign.EntityFrameworkCore;
using SystemManagement.AppService;
using SystemManagement.Domain;
using SystemManagement.Infrastructure.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace SystemManagement.Infrastructure
{
    [DependsOn(
        typeof(SystemManagementAppServiceModule),
        typeof(SystemManagemementDomainModule),
        typeof(RYDesignEntityFrameworkCoreModule)
        )]
    public class SystemManagementInfrastructureModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<SystemManagementDbContext>(options =>
            {
                options.AddDefaultRepositories(true);
            });
        }
    }
}
