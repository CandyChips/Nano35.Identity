using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.UseCase.GetUserById
{
    public class GetUserByIdConsumer : 
        IConsumer<IGetUserByIdRequestContract>
    {
        private readonly IServiceProvider  _services;
        public GetUserByIdConsumer(IServiceProvider services) { _services = services; }
        public async Task Consume(ConsumeContext<IGetUserByIdRequestContract> context)
        {
            var result = 
                await new LoggedRailPipeNode<IGetUserByIdRequestContract, IGetUserByIdSuccessResultContract>(
                    _services.GetService(typeof(ILogger<IGetUserByIdRequestContract>)) as ILogger<IGetUserByIdRequestContract>,  
                    new GetUserByIdUseCase(
                        _services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                    .Ask(context.Message, context.CancellationToken);
            await result.Match(
                async r => 
                    await context.RespondAsync(r),
                async e => 
                    await context.RespondAsync<IGetUserByIdErrorResultContract>(e));
        }
    }
}