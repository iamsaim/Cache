using System.Reflection;
using CacheManager.Application.Common.Contants;
using CacheManager.Infrastructure.Extension;
using CacheManager.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CacheManager.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configuring and registering appSettings.json
            services.RegisterAppSettings(Configuration);
            

            // Configuring CORS policies
            services.ConfigureCorsPolicy(Configuration, Contants.AllowAll);

            // Configuring services
            services.ConfigureService();

            // Configuring API version
            services.ConfigureVersion(ApiVersion.Default, true);

            // Configuring HTTP context
            services.AddHttpContextAccessor();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cache Manager", Version = "v1" });
                c.DocInclusionPredicate((version, desc) =>
                {
                    if (!desc.TryGetMethodInfo(out MethodInfo methodInfo))
                    {
                        return false;
                    }

                    var versions = methodInfo.DeclaringType
                        .GetCustomAttributes(true)
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions);

                    return versions.Any(v => $"v{v.ToString()}" == version);
                });
            });

            // Adding Exception filter
            services.AddControllers(options =>
                options.Filters.Add<ApiExceptionFilterAttribute>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Using CORS policy
            app.UseCors(Contants.AllowAll);

            // Using Custom logging middleware
            app.ConfigureCustomExceptionMiddleware();

            // Request logging
            app.ConfigureRequestLogging();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaxCalculator.API v1"));
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
