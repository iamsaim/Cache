using CacheManager.Application.Common.Middleware;
using Microsoft.AspNetCore.Builder;

namespace CacheManager.Infrastructure.Extension
{
    public static class ConfigurePipeline
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<UnhandledExceptionMiddleware>();
        }
    }
}