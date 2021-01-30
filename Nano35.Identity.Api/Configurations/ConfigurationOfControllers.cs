using Microsoft.Extensions.DependencyInjection;

namespace Nano35.Identity.Api.Configurations
{
    public class ConfigurationOfControllers : 
        IConfigurationOfService
    {
        public void AddToServices(
            IServiceCollection services)
        {
            services.AddControllers();
        }
    }
}