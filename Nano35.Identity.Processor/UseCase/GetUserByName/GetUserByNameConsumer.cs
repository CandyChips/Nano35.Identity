using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Models;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.UseCase.GetUserByName
{
    public class GetUserByNameConsumer : IConsumer<IGetUserByNameRequestContract>
    {
        private readonly IServiceProvider  _services;
        public GetUserByNameConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IGetUserByNameRequestContract> context)
        {
            var result = 
                await new LoggedPipeNode<IGetUserByNameRequestContract, IGetUserByNameResultContract>(
                    _services.GetService(typeof(ILogger<IGetUserByNameRequestContract>)) as ILogger<IGetUserByNameRequestContract>,  
                    new GetUserByName(
                        _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                        _services.GetService(typeof(UserManager<User>)) as UserManager<User>))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}