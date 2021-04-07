using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Models;
using Nano35.Identity.Processor.Services.Helpers;
using Nano35.Identity.Processor.UseCase.GetUserById;

namespace Nano35.Identity.Processor.UseCase.CreateUser
{
    public class CreateUserConsumer : 
        IConsumer<ICreateUserRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public CreateUserConsumer(
            IServiceProvider services)
        {
            _services = services;
        }

        public async Task Consume(
            ConsumeContext<ICreateUserRequestContract> context)
        {
            // Setup configuration of pipeline
            var userManager = (UserManager<User>) _services.GetService(typeof(UserManager<User>));
            var logger = (ILogger<ICreateUserRequestContract>) _services.GetService(typeof(ILogger<ICreateUserRequestContract>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result = 
                await new LoggedPipeNode<ICreateUserRequestContract, ICreateUserResultContract>(logger,
                    new ValidatedCreateUserRequest(
                        new CreateUserUseCase(userManager))
                ).Ask(message, context.CancellationToken);
            
            // Check response of create client request
            switch (result)
            {
                case ICreateUserSuccessResultContract:
                    await context.RespondAsync<ICreateUserSuccessResultContract>(result);
                    break;
                case ICreateUserErrorResultContract:
                    await context.RespondAsync<ICreateUserErrorResultContract>(result);
                    break;
            }
        }
    }
}