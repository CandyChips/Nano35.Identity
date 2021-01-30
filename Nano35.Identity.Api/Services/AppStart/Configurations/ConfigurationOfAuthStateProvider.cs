using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Identity.Api.Services.Helpers;

namespace Nano35.Identity.Api.Services.AppStart.Configurations
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