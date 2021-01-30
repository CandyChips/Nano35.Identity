using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Identity.Processor.Services.Helpers;
using Nano35.Identity.Processor.Services.MappingProfiles;

namespace Nano35.Identity.Processor.Services.AppStart.Configure
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