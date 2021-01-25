using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Identity.Consumers.Services.Configure;
using Nano35.Identity.Consumers.Services.Contexts;

namespace Nano35.Identity.Consumers
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            EntityFrameworkServiceConstructor.Construct(services);
            IdentityServiceConstructor.Construct(services);
            MassTransitServiceConstructor.Construct(services);
            AutoMapperServiceConstructor.Construct(services);
            services.AddTransient<IJwtGenerator, JwtGenerator>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public void Configure(IApplicationBuilder app)
        {
        }
    }
}
