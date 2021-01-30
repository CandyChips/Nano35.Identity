using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Requests;

namespace Nano35.Identity.Processor.Consumers
{
    public class RegisterConsumer : 
        IConsumer<IRegisterRequestContract>
    {
        private readonly ILogger<RegisterConsumer> _logger;
        private readonly MediatR.IMediator _mediator;
        public RegisterConsumer(
            ILogger<RegisterConsumer> logger, 
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        public async Task Consume(ConsumeContext<IRegisterRequestContract> context)
        {
            _logger.LogInformation("IRegisterRequestContract tracked");

            var message = context.Message;

            var request = new RegisterCommand()
            {
                NewUserId = message.NewUserId,
                Phone = message.Phone,
                Email = message.Email,
                Password = message.Password,
                PasswordConfirm = message.PasswordConfirm
            };

            var result = await _mediator.Send(request);
            
            switch (result)
            {
                case IRegisterSuccessResultContract:
                    await context.RespondAsync<IRegisterSuccessResultContract>(result);
                    break;
                case IRegisterErrorResultContract:
                    await context.RespondAsync<IRegisterErrorResultContract>(result);
                    break;
            }
        }
    }
}