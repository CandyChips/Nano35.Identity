using Microsoft.Extensions.DependencyInjection;

namespace Nano35.Identity.Api.Services.AppStart.Configurations
{
    public class CorsConfiguration : 
        IConfigurationOfService
    {
        public void AddToServices(
            IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("Cors", builder => 
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()));
        }
    }
}