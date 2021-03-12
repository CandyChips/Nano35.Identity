using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Api.Requests.GenerateToken;
using Nano35.Identity.Api.Requests.GetUserById;
using Nano35.Identity.Api.Requests.Register;

namespace Nano35.Identity.Api.Configurations
{
    public class ConfigurationOfFluidValidator:
        IConfigurationOfService
    {
        public void AddToServices(
            IServiceCollection services)
        {
            services.AddSingleton<IValidator<IGenerateTokenRequestContract>, ValidatorOfGenerateTokenRequest>();
            services.AddSingleton<IValidator<IGetUserByIdRequestContract>, ValidatorOfGetUserByIdRequest>();
            services.AddSingleton<IValidator<IRegisterRequestContract>, ValidatorOfRegisterRequest>();
        }
    }
}