using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Models;

namespace Nano35.Identity.Processor.UseCase.ConfirmEmailOfUser
{
    public class ConfirmEmailOfUserConsumer : IConsumer<IConfirmEmailOfUserRequestContract>
    {
        private readonly IServiceProvider  _services;
        public ConfirmEmailOfUserConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IConfirmEmailOfUserRequestContract> context)
        {
            var result = 
                await new LoggedUseCasePipeNode<IConfirmEmailOfUserRequestContract, IConfirmEmailOfUserResultContract>(
                    _services.GetService(typeof(ILogger<IConfirmEmailOfUserRequestContract>)) as ILogger<IConfirmEmailOfUserRequestContract>,  
                    new ConfirmEmailOfUserUseCase(
                        _services.GetService(typeof(UserManager<User>)) as UserManager<User>))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}