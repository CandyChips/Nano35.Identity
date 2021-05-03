using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Models;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.UseCase.UpdatePhone
{
    public class UpdatePhoneConsumer : IConsumer<IUpdatePhoneRequestContract>
    {
        private readonly IServiceProvider  _services;
        public UpdatePhoneConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(
            ConsumeContext<IUpdatePhoneRequestContract> context)
        {
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var result = 
                await new LoggedUseCasePipeNode<IUpdatePhoneRequestContract, IUpdatePhoneResultContract>(
                    _services.GetService(typeof(ILogger<IUpdatePhoneRequestContract>)) as ILogger<IUpdatePhoneRequestContract>,
                    new TransactedUseCasePipeNode<IUpdatePhoneRequestContract, IUpdatePhoneResultContract>(
                        dbContext,
                        new UpdatePhoneUseCase(dbContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}