using System;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Requests;
using Nano35.Identity.Processor.Requests.GetAllRoles;
using Nano35.Identity.Processor.Requests.GetAllUsers;
using Nano35.Identity.Processor.Requests.GetUserById;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.Consumers
{
    public class GetUserByIdConsumer : 
        IConsumer<IGetUserByIdRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public GetUserByIdConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(ConsumeContext<IGetUserByIdRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedGetUserByIdRequest>) _services.GetService(typeof(ILogger<LoggedGetUserByIdRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result = 
                await new LoggedGetUserByIdRequest(logger,  
                    new GetUserByIdValidator(
                        new GetUserByIdRequest(dbContext))
                    ).Ask(message, context.CancellationToken);
            
            // Check response of create client request
            switch (result)
            {
                case IGetUserByIdSuccessResultContract:
                    await context.RespondAsync<IGetUserByIdSuccessResultContract>(result);
                    break;
                case IGetUserByIdErrorResultContract:
                    await context.RespondAsync<IGetUserByIdErrorResultContract>(result);
                    break;
            }
        }
    }
}