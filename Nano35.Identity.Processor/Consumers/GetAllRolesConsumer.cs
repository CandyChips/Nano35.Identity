using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Requests;

namespace Nano35.Identity.Processor.Consumers
{
    public class GetAllRolesConsumer : 
        IConsumer<IGetAllRolesRequestContract>
    {
        private readonly ILogger<GetAllRolesConsumer> _logger;
        private readonly MediatR.IMediator _mediator;
        public GetAllRolesConsumer(
            ILogger<GetAllRolesConsumer> logger, 
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        public async Task Consume(ConsumeContext<IGetAllRolesRequestContract> context)
        {
            _logger.LogInformation("IGetAllUsersRequestContract tracked");

            var message = context.Message;

            var request = new GetAllRolesQuery();

            var result = await _mediator.Send(request);
            
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