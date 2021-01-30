using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Requests;

namespace Nano35.Identity.Processor.Consumers
{
    public class GenerateTokenConsumer : 
        IConsumer<IGenerateTokenRequestContract>
    {
        private readonly ILogger<GenerateTokenConsumer> _logger;
        private readonly MediatR.IMediator _mediator;

        public GenerateTokenConsumer(
            ILogger<GenerateTokenConsumer> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task Consume(
            ConsumeContext<IGenerateTokenRequestContract> context)
        {
            _logger.LogInformation("IGenerateTokenRequestContract tracked");

            var message = context.Message;

            var request = new GenerateTokenQuery()
            {
                Login = message.Login, 
                Password = message.Password
            };

            var result = await _mediator.Send(request);
            
            switch (result)
            {
                case IGenerateTokenSuccessResultContract:
                    await context.RespondAsync<IGenerateTokenSuccessResultContract>(result);
                    break;
                case IGenerateTokenErrorResultContract:
                    await context.RespondAsync<IGenerateTokenErrorResultContract>(result);
                    break;
            }
        }
    }
}