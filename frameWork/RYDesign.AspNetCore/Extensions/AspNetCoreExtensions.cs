using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RYDesignAspNetCore.Filter;

namespace RYDesignAspNetCore.Extensions;
public static class AspNetCoreExtensions
{
    #region 添加过滤器

    /// <summary>
    /// 统一返回格式过滤器
    /// </summary>
    /// <param name="services"></param>
    public static void ConfigurationFilters(this IServiceCollection services)
    {
        services.AddControllers(option =>
        {
            option.Filters.Add<HttpResponseExceptionFilter>();
            option.Filters.Add<HttpResponseSuccessFilter>();
        }).AddJsonOptions(options => {
            

            options.JsonSerializerOptions.PropertyNamingPolicy = null;
            //空字段不响应Response
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });
    }

    #endregion

    /// <summary>
    /// 设置跨域内容
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void ConfigurationUseCore(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.
                WithOrigins(
                    configuration["App:CorsOrigins"]?
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.RemovePostFix("/"))
                            .ToArray() ?? []
                    )
                    .WithAbpExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
    }

    public static void ConfigurationSwagger(this IServiceCollection services,string xml)
    {
        services.AddSwaggerGen(options =>
        {
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xml), true);
            
        });
    }


  

}
