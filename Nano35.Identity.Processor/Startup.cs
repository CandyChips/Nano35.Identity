using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Identity.Processor.Configurations;

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
        }

        public void Configure(IApplicationBuilder app)
        {
        }
    }
}
