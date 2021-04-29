using System;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Contracts;
using Nano35.Identity.Processor.UseCase.CreateUser;
using Nano35.Identity.Processor.UseCase.GenerateToken;
using Nano35.Identity.Processor.UseCase.GetAllUsers;
using Nano35.Identity.Processor.UseCase.GetUserById;
using Nano35.Identity.Processor.UseCase.GetUserByName;
using Nano35.Identity.Processor.UseCase.Register;

namespace Nano35.Identity.Processor.Configurations
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
                    
                    cfg.ReceiveEndpoint("IGetAllUsersRequestContract", e =>
                    {
                        e.Consumer<GetAllUsersConsumer>(provider);
                    });
                    
                    cfg.ReceiveEndpoint("IGetUserByIdRequestContract", e =>
                    {
                        e.Consumer<GetUserByIdConsumer>(provider);
                    });
                    cfg.ReceiveEndpoint("IGetUserByNameRequestContract", e =>
                    {
                        e.Consumer<GetUserByNameConsumer>(provider);
                    });
                    
                    cfg.ReceiveEndpoint("IRegisterRequestContract", e =>
                    {
                        e.Consumer<RegisterConsumer>(provider);
                    });
                    
                    cfg.ReceiveEndpoint("IGenerateTokenRequestContract", e =>
                    {
                        e.Consumer<GenerateTokenConsumer>(provider);
                    });
                    cfg.ReceiveEndpoint("ICreateUserRequestContract", e =>
                    {
                        e.Consumer<CreateUserConsumer>(provider);
                    });
                }));
                x.AddConsumer<GetAllUsersConsumer>();
                x.AddConsumer<CreateUserConsumer>();
                x.AddConsumer<GetUserByIdConsumer>();
                x.AddConsumer<GetUserByNameConsumer>();
                x.AddConsumer<RegisterConsumer>();
                x.AddConsumer<GenerateTokenConsumer>();
            });
            services.AddMassTransitHostedService();
        }
    }
}