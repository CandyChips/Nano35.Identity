using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Identity.Processor.Configurations;
using Nano35.Identity.Processor.Services.Helpers;

namespace Nano35.Identity.Processor
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
            new Configurator(services, new IdentityConfiguration()).Configure();
            new Configurator(services, new AutoMapperConfiguration()).Configure();
            new Configurator(services, new EntityFrameworkConfiguration()).Configure();
            new Configurator(services, new MassTransitConfiguration()).Configure();
            new Configurator(services, new JWTGeneratorConfiguration()).Configure();
            new Configurator(services, new MediatRConfiguration()).Configure();
        }

        public void Configure(IApplicationBuilder app)
        {
        }
    }
}
