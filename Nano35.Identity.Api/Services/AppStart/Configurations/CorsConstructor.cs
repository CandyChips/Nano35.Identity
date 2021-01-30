using Microsoft.Extensions.DependencyInjection;

namespace Nano35.Identity.Api.Services.AppStart.Configurations
{
    public static class CorsServiceConstructor
    {
        public static void Construct(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("Cors", builder => 
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()));
        }
    }
}