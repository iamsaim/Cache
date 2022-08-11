using Microsoft.Extensions.DependencyInjection;

namespace CacheManager.Infrastructure.Extension
{
    public static class ConfigureMapper
    {
        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services, Type assembly)
        {
            return services.AddAutoMapper(assembly);
        }
    }
}