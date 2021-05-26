using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Models;
using Nano35.Identity.Processor.Services.Helpers;

namespace Nano35.Identity.Processor.UseCase.GetUserClaimsByLogin
{
    public class GetUserClaimsByLoginConsumer : IConsumer<IGetUserClaimsByLoginRequestContract>
    {
        private readonly IServiceProvider  _services;
        public GetUserClaimsByLoginConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IGetUserClaimsByLoginRequestContract> context)
        {
            var result = 
                await new LoggedPipeNode<IGetUserClaimsByLoginRequestContract, IGetUserClaimsByLoginResultContract>(
                    _services.GetService(typeof(ILogger<IGetUserClaimsByLoginRequestContract>)) as ILogger<IGetUserClaimsByLoginRequestContract>,  
                    new GetUserClaimsByLogin(
                        _services.GetService(typeof(UserManager<User>)) as UserManager<User>, 
                        _services.GetService(typeof(SignInManager<User>)) as SignInManager<User>))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}