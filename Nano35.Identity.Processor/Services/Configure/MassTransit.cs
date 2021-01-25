using System;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Contracts;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Consumers.Services.MassTransit.Consumers;

namespace Nano35.Identity.Consumers.Services.Configure
{
    public static class MassTransitServiceConstructor
    {
        public static void Construct(
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
                    
                    cfg.ReceiveEndpoint("IGetAllUsersRequestContract", e =>
                    {
                        e.Consumer<GetAllUsersConsumer>(provider);
                    });
                    
                    cfg.ReceiveEndpoint("IGetAllRolesRequestContract", e =>
                    {
                        e.Consumer<GetAllRolesConsumer>(provider);
                    });
                    
                    cfg.ReceiveEndpoint("IGetUserByIdRequestContract", e =>
                    {
                        e.Consumer<GetUserByIdConsumer>(provider);
                    });
                    
                    cfg.ReceiveEndpoint("IGetRoleByIdRequestContract", e =>
                    {
                        e.Consumer<GetRoleByIdConsumer>(provider);
                    });
                    
                    cfg.ReceiveEndpoint("IRegisterRequestContract", e =>
                    {
                        e.Consumer<RegisterConsumer>(provider);
                    });
                    
                    cfg.ReceiveEndpoint("IGenerateTokenRequestContract", e =>
                    {
                        e.Consumer<GenerateTokenConsumer>(provider);
                    });
                }));
                x.AddConsumer<GetAllUsersConsumer>();
                x.AddConsumer<GetAllRolesConsumer>();
                x.AddConsumer<GetUserByIdConsumer>();
                x.AddConsumer<GetRoleByIdConsumer>();
                x.AddConsumer<RegisterConsumer>();
                x.AddConsumer<GenerateTokenConsumer>();
            });
            services.AddMassTransitHostedService();
        }
    }
}