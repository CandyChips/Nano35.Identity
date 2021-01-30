using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Requests;

namespace Nano35.Identity.Processor.Consumers
{
    public class GetRoleByIdConsumer : 
        IConsumer<IGetRoleByIdRequestContract>
    {
        private readonly ILogger<GetRoleByIdConsumer> _logger;
        private readonly MediatR.IMediator _mediator;
        public GetRoleByIdConsumer(
            ILogger<GetRoleByIdConsumer> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        public async Task Consume(ConsumeContext<IGetRoleByIdRequestContract> context)
        {
            _logger.LogInformation("IGetRoleByIdRequestContract tracked");

            var message = context.Message;

            var request = new GetRoleByIdQuery()
            {
                RoleId = message.RoleId
            };

            var result = await _mediator.Send(request);
            
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