using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Api.Configurations;
using Nano35.Identity.Api.Middlewares;

namespace Nano35.Identity.Api
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
            new Configurator(services, new AuthenticationConfiguration()).Configure();
            new Configurator(services, new CorsConfiguration()).Configure();
            new Configurator(services, new SwaggerConfiguration()).Configure();
            new Configurator(services, new MassTransitConfiguration()).Configure();
            new Configurator(services, new ConfigurationOfAuthStateProvider()).Configure();
            new Configurator(services, new ConfigurationOfControllers()).Configure();
            new Configurator(services, new ConfigurationOfFluidValidator()).Configure();
            
            services.AddHealthChecks();
        }

        public void Configure(IApplicationBuilder app)
        {
            ConfigureCommon.Configure(app);
            ConfigureEndpoints.Configure(app);
        }
    }
}
