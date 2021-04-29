using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Identity.Processor.Configurations;

namespace Nano35.Identity.Processor
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        
        public Startup()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("ServicesConfig.json");
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            new Configurator(services, new IdentityConfiguration()).Configure();
            new Configurator(services, new EntityFrameworkConfiguration(Configuration)).Configure();
            new Configurator(services, new MassTransitConfiguration()).Configure();
            new Configurator(services, new JWTGeneratorConfiguration()).Configure();
        }

        public void Configure(IApplicationBuilder app)
        {
        }
    }
}
