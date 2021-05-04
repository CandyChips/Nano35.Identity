using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Models;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.UseCase.UpdateEmail
{
    public class UpdateEmailConsumer : 
        IConsumer<IUpdateEmailRequestContract>
    {
        private readonly IServiceProvider  _services;
        public UpdateEmailConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IUpdateEmailRequestContract> context)
        {
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var result = 
                await new LoggedPipeNode<IUpdateEmailRequestContract, IUpdateEmailResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateEmailRequestContract>)) as ILogger<IUpdateEmailRequestContract>,
                    new TransactedPipeNode<IUpdateEmailRequestContract, IUpdateEmailResultContract>(
                        dbContext,
                        new UpdateEmail(dbContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}