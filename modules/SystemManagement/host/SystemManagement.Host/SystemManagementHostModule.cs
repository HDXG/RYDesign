using Microsoft.EntityFrameworkCore;
using RYDesignAspNetCore.Extensions;
using Swashbuckle.AspNetCore.SwaggerUI;
using SystemManagement.Domain;
using SystemManagement.HttpApi;
using SystemManagement.Infrastructure;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Autofac;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace SystemManagement.Host
{
    [DependsOn(

        typeof(SystemManagementHttpApiModule),
        typeof(SystemManagementInfrastructureModule),
        typeof(AbpAutofacModule)

        )]
    public class SystemManagementHostModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostEnvironment = context.Services.GetAbpHostEnvironment();
            Configure<AbpDbContextOptions>(options =>
            {
                options.Configure(dbConfigContext =>
                {
                    // 本地研发环境 - 输出到控制台
                    if (hostEnvironment.EnvironmentName == "Development")
                    {
                        dbConfigContext.DbContextOptions.LogTo(Serilog.Log.Information, new[] { DbLoggerCategory.Database.Command.Name }).EnableSensitiveDataLogging();
                    }
                    
                });
            });

            // 日志
            Configure<AbpAuditingOptions>(opt =>
            {
                opt.ApplicationName = SystemManagemementConsts.ApplicationName;
                opt.IsEnabledForGetRequests = true;
            });

            //
            context.Services.ConfigurationFilters();
            context.Services.ConfigurationUseCore(configuration);
            context.Services.ConfigurationSwagger("SystemManagement.HttpApi.xml");
        }


        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCorrelationId();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.DocExpansion(DocExpansion.None);
                options.DefaultModelExpandDepth(-1);
            });

            //app.UseAuthentication();
            //app.UseAuthorization();

            app.UseAuditing();
            app.UseConfiguredEndpoints(endpoints =>
            {
                // AuthorizeAttribute
                // endpoints.MapControllers().RequireAuthorization();
            });
        }
    }
}
