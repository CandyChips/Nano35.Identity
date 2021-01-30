using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Nano35.Identity.Api.Configurations
{
    public class SwaggerConfiguration : 
        IConfigurationOfService
    {
        public void AddToServices(
            IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Identity API",
                    Description = "A entrypoint to identity microservice",
                    Contact = new OpenApiContact
                    {
                        Name = "Guihub folder",
                        Email = string.Empty,
                        Url = new Uri("https://github.com/CandyChips/Nano35.Identity"),
                    }
                });
            });
        }
    }
}