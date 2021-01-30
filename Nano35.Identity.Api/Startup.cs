using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nano35.Identity.Api.Services.AppStart.Configurations;
using Nano35.Identity.Api.Services.AppStart.ConfigureServices;
using Nano35.Identity.Api.Services.Constructors;
using Nano35.Identity.Api.Services.Helpers;
using Nano35.Identity.Api.Services.Middlewares;

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
            new Configurator(services, new MediatRConfiguration()).Configure();
            new Configurator(services, new ConfigurationOfAuthStateProvider()).Configure();
            new Configurator(services, new ConfigurationOfControllers()).Configure();
        }

        public void Configure(IApplicationBuilder app)
        {
            ConfigureCommon.Configure(app);
            ConfigureEndpoints.Configure(app);
        }
    }
}
