using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Models;

namespace Nano35.Identity.Processor.UseCase.UpdatePassword
{
    public class UpdatePasswordConsumer : IConsumer<IUpdatePasswordRequestContract>
    {
        private readonly IServiceProvider  _services;
        public UpdatePasswordConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IUpdatePasswordRequestContract> context)
        {
            var result = 
                await new LoggedUseCasePipeNode<IUpdatePasswordRequestContract, IUpdatePasswordResultContract>(
                    _services.GetService(typeof(ILogger<IUpdatePasswordRequestContract>)) as ILogger<IUpdatePasswordRequestContract>,
                    new UpdatePasswordUseCase())
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}