using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Models;

namespace Nano35.Identity.Processor.UseCase.UpdateEmail
{
    public class UpdateEmailConsumer : 
        IConsumer<IUpdateEmailRequestContract>
    {
        private readonly IServiceProvider  _services;
        public UpdateEmailConsumer(IServiceProvider services) { _services = services; }
        public async Task Consume(ConsumeContext<IUpdateEmailRequestContract> context)
        {
            var result = 
                await new LoggedRailPipeNode<IUpdateEmailRequestContract, IUpdateEmailSuccessResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateEmailRequestContract>)) as ILogger<IUpdateEmailRequestContract>,
                    new UpdateEmailUseCase(_services.GetService(typeof(UserManager<User>)) as UserManager<User>)).Ask(context.Message, context.CancellationToken);
            await result.Match(
                async r => 
                    await context.RespondAsync(r),
                async e => 
                    await context.RespondAsync<IUpdateEmailErrorResultContract>(e));
        }
    }
}