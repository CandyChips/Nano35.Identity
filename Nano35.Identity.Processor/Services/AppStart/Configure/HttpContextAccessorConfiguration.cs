using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Nano35.Identity.Processor.Services.AppStart.Configure
{
    public class HttpContextAccessorConfiguration : 
        IConfigurationOfService
    {
        public void AddToServices(
            IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}