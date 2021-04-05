using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Models;
using Nano35.Identity.Processor.Services.Helpers;

namespace Nano35.Identity.Processor.UseCase.GenerateToken
{
    public class GenerateTokenConsumer : 
        IConsumer<IGenerateTokenRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public GenerateTokenConsumer(
            IServiceProvider services)
        {
            _services = services;
        }

        public async Task Consume(
            ConsumeContext<IGenerateTokenRequestContract> context)
        {
            var result = 
                await new LoggedPipeNode<IGenerateTokenRequestContract, IGenerateTokenResultContract>(
                    _services.GetService(typeof(ILogger<IGenerateTokenRequestContract>)) as ILogger<IGenerateTokenRequestContract>,  
                    new GenerateTokenUseCase(
                        _services.GetService(typeof(UserManager<User>)) 
                            as UserManager<User>, 
                        _services.GetService(typeof(SignInManager<User>)) 
                            as SignInManager<User>, 
                        _services.GetService(typeof(IJwtGenerator)) 
                            as IJwtGenerator))
                    .Ask(context.Message, context.CancellationToken);
            switch (result)
            {
                case IGenerateTokenSuccessResultContract:
                    await context.RespondAsync<IGenerateTokenSuccessResultContract>(result);
                    break;
                case IGenerateTokenErrorResultContract:
                    await context.RespondAsync<IGenerateTokenErrorResultContract>(result);
                    break;
            }
        }
    }
}