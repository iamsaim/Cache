using CacheManager.Application.Common.Contracts;
using CacheManager.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CacheManager.Infrastructure.Extension
{
    public static class ConfigureServices
    {
        public static IServiceCollection ConfigureService(this IServiceCollection services)
        {
            services.AddSingleton<ICacheService, CacheService>();

            return services;
        }
    }
}
