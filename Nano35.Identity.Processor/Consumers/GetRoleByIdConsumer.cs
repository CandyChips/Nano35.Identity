using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Requests.GetRoleById;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.Consumers
{
    public class GetRoleByIdConsumer : 
        IConsumer<IGetRoleByIdRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public GetRoleByIdConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetRoleByIdRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedGetRoleByIdRequest>) _services.GetService(typeof(ILogger<LoggedGetRoleByIdRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result = 
                await new LoggedGetRoleByIdRequest(logger,  
                    new ValidatedGetRoleByIdRequest(
                        new GetRoleByIdRequest(dbContext))
                    ).Ask(message, context.CancellationToken);
            
            // Check response of create client request
            switch (result)
            {
                case IGetRoleByIdSuccessResultContract:
                    await context.RespondAsync<IGetRoleByIdSuccessResultContract>(result);
                    break;
                case IGetRoleByIdErrorResultContract:
                    await context.RespondAsync<IGetRoleByIdErrorResultContract>(result);
                    break;
            }
        }
    }
}