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
            // Setup configuration of pipeline
            var userManager = (UserManager<User>) _services.GetService(typeof(UserManager<User>));
            var signInManager = (SignInManager<User>) _services.GetService(typeof(SignInManager<User>));
            var jwtGenerator = (IJwtGenerator) _services.GetService(typeof(IJwtGenerator));
            var logger = (ILogger<IGenerateTokenRequestContract>) _services.GetService(typeof(ILogger<IGenerateTokenRequestContract>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result = 
                await new  LoggedPipeNode<IGenerateTokenRequestContract, IGenerateTokenResultContract>(logger, 
                    new ValidatedGenerateTokenRequest(
                        new GenerateTokenUseCase(userManager, signInManager, jwtGenerator))
                ).Ask(message, context.CancellationToken);
            
            // Check response of create client request
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