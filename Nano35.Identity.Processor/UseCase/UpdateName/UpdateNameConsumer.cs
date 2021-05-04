using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.UseCase.UpdateName
{
    public class UpdateNameConsumer : IConsumer<IUpdateNameRequestContract>
    {
        private readonly IServiceProvider  _services;
        public UpdateNameConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IUpdateNameRequestContract> context)
        {
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var result = 
                await new LoggedPipeNode<IUpdateNameRequestContract, IUpdateNameResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateNameRequestContract>)) as ILogger<IUpdateNameRequestContract>,
                    new TransactedPipeNode<IUpdateNameRequestContract, IUpdateNameResultContract>(
                        dbContext,
                        new UpdateName(
                            dbContext))
                    )
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);

        }
    }
}