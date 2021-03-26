using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Models;
using Nano35.Identity.Processor.Services.Helpers;

namespace Nano35.Identity.Processor.UseCase.ConfirmEmailOfUser
{
    public class ConfirmEmailOfUserConsumer : 
        IConsumer<IConfirmEmailOfUserRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public ConfirmEmailOfUserConsumer(
            IServiceProvider services)
        {
            _services = services;
        }

        public async Task Consume(
            ConsumeContext<IConfirmEmailOfUserRequestContract> context)
        {
            // Setup configuration of pipeline
            var userManager = (UserManager<User>) _services.GetService(typeof(UserManager<User>));
            var logger = (ILogger<LoggedConfirmEmailOfUserRequest>) _services.GetService(typeof(ILogger<LoggedConfirmEmailOfUserRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result = 
                await new LoggedConfirmEmailOfUserRequest(logger,  
                    new ValidatedConfirmEmailOfUserRequest(
                        new ConfirmEmailOfUserUseCase(userManager))
                ).Ask(message, context.CancellationToken);
            
            // Check response of create client request
            switch (result)
            {
                case IConfirmEmailOfUserSuccessResultContract:
                    await context.RespondAsync<IConfirmEmailOfUserSuccessResultContract>(result);
                    break;
                case IConfirmEmailOfUserErrorResultContract:
                    await context.RespondAsync<IConfirmEmailOfUserErrorResultContract>(result);
                    break;
            }
        }
    }
}