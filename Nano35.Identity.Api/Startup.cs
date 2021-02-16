using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Api.Configurations;
using Nano35.Identity.Api.ConfigureMiddleWares;
using Nano35.Identity.Api.Requests.GetRoleById;
using Nano35.Identity.Api.Requests.GetRoleByUserId;
using Nano35.Identity.Api.Requests.GetUserById;
using Nano35.Identity.Api.Requests.GetUsersByRoleId;
using Nano35.Identity.Api.Requests.Register;
using Nano35.Identity.Api.Validators;

namespace Nano35.Identity.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            new Configurator(services, new AuthenticationConfiguration()).Configure();
            new Configurator(services, new CorsConfiguration()).Configure();
            new Configurator(services, new SwaggerConfiguration()).Configure();
            new Configurator(services, new MassTransitConfiguration()).Configure();
            new Configurator(services, new ConfigurationOfAuthStateProvider()).Configure();
            new Configurator(services, new ConfigurationOfControllers()).Configure();
            new Configurator(services, new ConfigurationOfFluidValidator()).Configure();
            services.AddSingleton<IValidator<IGenerateTokenRequestContract>, ValidatorOfGenerateTokenRequest>();
            services.AddSingleton<IValidator<IGetRoleByIdRequestContract>, ValidatorOfGetRoleByIdRequest>();
            services.AddSingleton<IValidator<IGetRoleByUserIdRequestContract>, ValidatorOfGetRoleByUserIdRequest>();
            services.AddSingleton<IValidator<IGetUserByIdRequestContract>, ValidatorOfGetUserByIdRequest>();
            services.AddSingleton<IValidator<IRegisterRequestContract>, ValidatorOfRegisterRequest>();
            services.AddSingleton<IValidator<IGetUsersByRoleIdRequestContract>, ValidatorOfGetUsersByRoleIdRequest>();

        }

        public void Configure(IApplicationBuilder app)
        {
            ConfigureCommon.Configure(app);
            ConfigureEndpoints.Configure(app);
        }
    }
}
