using System;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Contracts;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Configurations
{
    public class MassTransitConfiguration : 
        IConfigurationOfService
    {
        public void AddToServices(
            IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.UseHealthCheck(provider);
                    cfg.Host(new Uri($"{ContractBase.RabbitMqLocation}/"), h =>
                    {
                        h.Username(ContractBase.RabbitMqUsername);
                        h.Password(ContractBase.RabbitMqPassword);
                    });
                }));
                
                x.AddRequestClient<ICreateUserRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/ICreateUserRequestContract"));
                x.AddRequestClient<IConfirmEmailOfUserRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IConfirmEmailOfUserRequestContract"));
                x.AddRequestClient<IGetUserByIdRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IGetUserByIdRequestContract"));
                x.AddRequestClient<IGetAllUsersRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IGetAllUsersRequestContract"));
                x.AddRequestClient<IGenerateTokenRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IGenerateTokenRequestContract"));
                x.AddRequestClient<IRegisterRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IRegisterRequestContract"));
                x.AddRequestClient<IUpdatePhoneRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IUpdatePhoneRequestContract"));
                x.AddRequestClient<IUpdatePasswordRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IUpdatePasswordRequestContract"));
                x.AddRequestClient<IUpdateEmailRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IUpdateEmailRequestContract"));
                x.AddRequestClient<IUpdateNameRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IUpdateNameRequestContract"));
            });
            services.AddMassTransitHostedService();
        }
    }
}