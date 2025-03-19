using System.Text.Json.Serialization;
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
}
