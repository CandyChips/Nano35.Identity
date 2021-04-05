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
        
        public UpdateEmailConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IUpdateEmailRequestContract> context)
        {
            var result = 
                await new LoggedPipeNode<IUpdateEmailRequestContract, IUpdateEmailResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateEmailRequestContract>)) as ILogger<IUpdateEmailRequestContract>,
                    new UpdateEmailUseCase(_services.GetService(typeof(UserManager<User>)) as UserManager<User>)).Ask(context.Message, context.CancellationToken);
            switch (result)
            {
                case IUpdateEmailSuccessResultContract:
                    await context.RespondAsync<IUpdateEmailSuccessResultContract>(result);
                    break;
                case IUpdateEmailErrorResultContract:
                    await context.RespondAsync<IUpdateEmailErrorResultContract>(result);
                    break;
            }
        }
    }
}