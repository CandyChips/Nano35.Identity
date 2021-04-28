using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.UseCase.GetUserByName
{
    public class GetUserByNameConsumer : 
        IConsumer<IGetUserByNameRequestContract>
    {
        private readonly IServiceProvider  _services;
        public GetUserByNameConsumer(IServiceProvider services) { _services = services; }
        public async Task Consume(ConsumeContext<IGetUserByNameRequestContract> context)
        {
            var result = 
                await new LoggedRailPipeNode<IGetUserByNameRequestContract, IGetUserByNameSuccessResultContract>(
                    _services.GetService(typeof(ILogger<IGetUserByNameRequestContract>)) as ILogger<IGetUserByNameRequestContract>,  
                    new GetUserByNameUseCase(
                        _services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                    .Ask(context.Message, context.CancellationToken);
            await result.Match(
                async r => 
                    await context.RespondAsync(r),
                async e => 
                    await context.RespondAsync<IGetUserByNameErrorResultContract>(e));
        }
    }
}