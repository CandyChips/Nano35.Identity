using System;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Requests;
using Nano35.Identity.Processor.Requests.GetAllUsers;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.Consumers
{
    public class GetAllUsersConsumer : 
        IConsumer<IGetAllUsersRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public GetAllUsersConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(ConsumeContext<IGetAllUsersRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedGetAllUsersRequest>) _services.GetService(typeof(ILogger<LoggedGetAllUsersRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result = 
                await new LoggedGetAllUsersRequest(logger,  
                    new ValidatedGetAllUsersRequest(
                        new GetAllUsersRequest(dbContext))
                    ).Ask(message, context.CancellationToken);
            
            // Check response of create client request
            switch (result)
            {
                case IGetAllUsersSuccessResultContract:
                    await context.RespondAsync<IGetAllUsersSuccessResultContract>(result);
                    break;
                case IGetAllUsersErrorResultContract:
                    await context.RespondAsync<IGetAllUsersErrorResultContract>(result);
                    break;
            }
        }
    }
}