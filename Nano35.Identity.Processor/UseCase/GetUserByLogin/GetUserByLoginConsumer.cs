using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Models;
using Nano35.Identity.Processor.Services.Helpers;

namespace Nano35.Identity.Processor.UseCase.GetUserByLogin
{
    public class GetUserByLoginConsumer : IConsumer<IGetUserByLoginRequestContract>
    {
        private readonly IServiceProvider  _services;
        public GetUserByLoginConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IGetUserByLoginRequestContract> context)
        {
            var result = 
                await new LoggedPipeNode<IGetUserByLoginRequestContract, IGetUserByLoginResultContract>(
                    _services.GetService(typeof(ILogger<IGetUserByLoginRequestContract>)) as ILogger<IGetUserByLoginRequestContract>,  
                    new GetUserByLogin(
                        _services.GetService(typeof(UserManager<User>)) as UserManager<User>, 
                        _services.GetService(typeof(SignInManager<User>)) as SignInManager<User>, 
                        _services.GetService(typeof(IJwtGenerator)) as IJwtGenerator))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}