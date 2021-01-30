using Microsoft.Extensions.DependencyInjection;
using Nano35.Identity.Processor.Services.Helpers;

namespace Nano35.Identity.Processor.Configurations
{
    public class JWTGeneratorConfiguration : 
        IConfigurationOfService
    {
        public void AddToServices(
            IServiceCollection services)
        {
            services.AddTransient<IJwtGenerator, JwtGenerator>();
        }
    }
}