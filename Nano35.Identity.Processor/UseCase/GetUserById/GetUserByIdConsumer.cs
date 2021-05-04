using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Models;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.UseCase.GetUserById
{
    public class GetUserByIdConsumer : IConsumer<IGetUserByIdRequestContract>
    {
        private readonly IServiceProvider  _services;
        public GetUserByIdConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IGetUserByIdRequestContract> context)
        {
            var result = 
                await new LoggedPipeNode<IGetUserByIdRequestContract, IGetUserByIdResultContract>(
                    _services.GetService(typeof(ILogger<IGetUserByIdRequestContract>)) as ILogger<IGetUserByIdRequestContract>,  
                    new GetUserById(
                        _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                        _services.GetService(typeof(UserManager<User>)) as UserManager<User>))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}