using Microsoft.Extensions.DependencyInjection;

namespace Nano35.Identity.Api.Services.AppStart.Configurations
{
    public interface IConfigurationOfService
    {
        void AddToServices(IServiceCollection services);
    }
    
    public class Configurator
    {
        private readonly IServiceCollection _services;
        private readonly IConfigurationOfService _configuration;
        public Configurator(
            IServiceCollection services, 
            IConfigurationOfService configuration)
        {
            _services = services;
            _configuration = configuration;
        }

        public void Configure()
        {
            _configuration.AddToServices(_services);
        }
    }
}