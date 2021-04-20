using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Models;

namespace Nano35.Identity.Processor.UseCase.UpdatePhone
{
    public class UpdatePhoneConsumer : 
        IConsumer<IUpdatePhoneRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdatePhoneConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IUpdatePhoneRequestContract> context)
        {
            var result = 
                await new LoggedRailPipeNode<IUpdatePhoneRequestContract, IUpdatePhoneSuccessResultContract>(
                    _services.GetService(typeof(ILogger<IUpdatePhoneRequestContract>)) as ILogger<IUpdatePhoneRequestContract>,
                    new UpdatePhoneUseCase(
                        _services.GetService(typeof(UserManager<User>)) as UserManager<User>))
                    .Ask(context.Message, context.CancellationToken);
            await result.Match(
                async r => 
                    await context.RespondAsync(r),
                async e => 
                    await context.RespondAsync<IUpdatePhoneErrorResultContract>(e));

        }
    }
}