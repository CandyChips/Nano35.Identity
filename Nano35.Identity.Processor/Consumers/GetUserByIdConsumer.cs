using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Requests;

namespace Nano35.Identity.Processor.Consumers
{
    public class GetUserByIdConsumer : 
        IConsumer<IGetUserByIdRequestContract>
    {
        private readonly ILogger<GetUserByIdConsumer> _logger;
        private readonly MediatR.IMediator _mediator;
        
        public GetUserByIdConsumer(
            ILogger<GetUserByIdConsumer> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        public async Task Consume(ConsumeContext<IGetUserByIdRequestContract> context)
        {
            _logger.LogInformation("IGetUserByIdRequestContract tracked");

            var message = context.Message;

            var request = new GetUserByIdQuery()
            {
                UserId = message.UserId
            };

            var result = await _mediator.Send(request);
            
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