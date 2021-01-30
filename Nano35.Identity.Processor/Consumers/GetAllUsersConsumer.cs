using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Requests;

namespace Nano35.Identity.Processor.Consumers
{
    public class GetAllUsersConsumer : 
        IConsumer<IGetAllUsersRequestContract>
    {
        private readonly ILogger<GetAllUsersConsumer> _logger;
        private readonly MediatR.IMediator _mediator;
        public GetAllUsersConsumer(
            ILogger<GetAllUsersConsumer> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        public async Task Consume(ConsumeContext<IGetAllUsersRequestContract> context)
        {
            _logger.LogInformation("IGetAllUsersRequestContract tracked");

            var message = context.Message;

            var request = new GetAllUsersQuery();

            var result = await _mediator.Send(request);
            
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