using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.UseCase.GetUserById
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
            var logger = (ILogger<IGetUserByIdRequestContract>) _services.GetService(typeof(ILogger<IGetUserByIdRequestContract>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result = 
                await new LoggedPipeNode<IGetUserByIdRequestContract, IGetUserByIdResultContract>(logger,
                        new GetUserByIdUseCase(dbContext)
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