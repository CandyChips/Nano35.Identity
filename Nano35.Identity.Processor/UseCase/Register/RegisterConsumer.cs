using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Models;

namespace Nano35.Identity.Processor.UseCase.Register
{
    public class RegisterConsumer : 
        IConsumer<IRegisterRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public RegisterConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IRegisterRequestContract> context)
        {
            // Setup configuration of pipeline
            var userManager = (UserManager<User>) _services.GetService(typeof(UserManager<User>));
            var logger = (ILogger<IRegisterRequestContract>) _services.GetService(typeof(ILogger<IRegisterRequestContract>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result = 
                await new LoggedPipeNode<IRegisterRequestContract, IRegisterResultContract>(logger,
                        new RegisterUseCase(userManager)
                    ).Ask(message, context.CancellationToken);
            
            // Check response of create client request
            switch (result)
            {
                case IRegisterSuccessResultContract:
                    await context.RespondAsync<IRegisterSuccessResultContract>(result);
                    break;
                case IRegisterErrorResultContract:
                    await context.RespondAsync<IRegisterErrorResultContract>(result);
                    break;
            }
        }
    }
}