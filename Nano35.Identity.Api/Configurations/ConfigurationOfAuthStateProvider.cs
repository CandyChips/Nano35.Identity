using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Identity.Api.Helpers;

namespace Nano35.Identity.Api.Configurations
{
    public class ConfigurationOfAuthStateProvider : 
        IConfigurationOfService
    {
        public void AddToServices(
            IServiceCollection services)
        {
            services.AddScoped<ICustomAuthStateProvider, CookiesAuthStateProvider>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}