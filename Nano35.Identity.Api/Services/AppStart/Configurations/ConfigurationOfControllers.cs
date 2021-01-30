using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Nano35.Identity.Api.Services.Helpers;

namespace Nano35.Identity.Api.Services.AppStart.Configurations
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