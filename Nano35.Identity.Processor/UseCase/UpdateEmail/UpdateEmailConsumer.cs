using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Models;

namespace Nano35.Identity.Processor.UseCase.UpdateEmail
{
    public class UpdateEmailConsumer : 
        IConsumer<IUpdateEmailRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdateEmailConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IUpdateEmailRequestContract> context)
        {
            // Setup configuration of pipeline
            var userManager = (UserManager<User>) _services.GetService(typeof(UserManager<User>));
            var logger = (ILogger<LoggedUpdateEmailRequest>) _services.GetService(typeof(ILogger<LoggedUpdateEmailRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result = 
                await new LoggedUpdateEmailRequest(logger,
                    new ValidatedUpdateEmailRequest(
                        new UpdateEmailRequest(userManager))
                    ).Ask(message, context.CancellationToken);
            
            // Check response of create client request
            switch (result)
            {
                case IUpdateEmailSuccessResultContract:
                    await context.RespondAsync<IUpdateEmailSuccessResultContract>(result);
                    break;
                case IUpdateEmailErrorResultContract:
                    await context.RespondAsync<IUpdateEmailErrorResultContract>(result);
                    break;
            }
        }
    }
}