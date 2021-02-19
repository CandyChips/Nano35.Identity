using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Requests.GetAllRoles;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.Consumers
{
    public class GetAllRolesConsumer : 
        IConsumer<IGetAllRolesRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public GetAllRolesConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(ConsumeContext<IGetAllRolesRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedGetAllRolesRequest>) _services.GetService(typeof(ILogger<LoggedGetAllRolesRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result = 
                await new LoggedGetAllRolesRequest(logger,  
                    new GetAllRolesRequest(dbContext)
                ).Ask(message, context.CancellationToken);
            
            // Check response of create client request
            switch (result)
            {
                case IGetAllRolesSuccessResultContract:
                    await context.RespondAsync<IGetAllRolesSuccessResultContract>(result);
                    break;
                case IGetAllRolesErrorResultContract:
                    await context.RespondAsync<IGetAllRolesErrorResultContract>(result);
                    break;
            }
        }
    }
}