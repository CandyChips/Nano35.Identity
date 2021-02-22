using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Models;
using Nano35.Identity.Processor.Requests.UpdateName;

namespace Nano35.Identity.Processor.Consumers
{
    public class UpdateNameConsumer : 
        IConsumer<IUpdateNameRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdateNameConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IUpdateNameRequestContract> context)
        {
            // Setup configuration of pipeline
            var userManager = (UserManager<User>) _services.GetService(typeof(UserManager<User>));
            var logger = (ILogger<LoggedUpdateNameRequest>) _services.GetService(typeof(ILogger<LoggedUpdateNameRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result = 
                await new LoggedUpdateNameRequest(logger,
                    new ValidatedUpdateNameRequest(
                        new UpdateNameRequest(userManager))
                    ).Ask(message, context.CancellationToken);
            
            // Check response of create client request
            switch (result)
            {
                case IUpdateNameSuccessResultContract:
                    await context.RespondAsync<IUpdateNameSuccessResultContract>(result);
                    break;
                case IUpdateNameErrorResultContract:
                    await context.RespondAsync<IUpdateNameErrorResultContract>(result);
                    break;
            }
        }
    }
}