using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Models;

namespace Nano35.Identity.Processor.UseCase.UpdatePhone
{
    public class UpdatePhoneConsumer : 
        IConsumer<IUpdatePhoneRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdatePhoneConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IUpdatePhoneRequestContract> context)
        {
            // Setup configuration of pipeline
            var userManager = (UserManager<User>) _services.GetService(typeof(UserManager<User>));
            var logger = (ILogger<LoggedUpdatePhoneRequest>) _services.GetService(typeof(ILogger<LoggedUpdatePhoneRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result = 
                await new LoggedUpdatePhoneRequest(logger,
                    new ValidatedUpdatePhoneRequest(
                        new UpdatePhoneRequest(userManager))
                    ).Ask(message, context.CancellationToken);
            
            // Check response of create client request
            switch (result)
            {
                case IUpdatePhoneSuccessResultContract:
                    await context.RespondAsync<IUpdatePhoneSuccessResultContract>(result);
                    break;
                case IUpdatePhoneErrorResultContract:
                    await context.RespondAsync<IUpdatePhoneErrorResultContract>(result);
                    break;
            }
        }
    }
}