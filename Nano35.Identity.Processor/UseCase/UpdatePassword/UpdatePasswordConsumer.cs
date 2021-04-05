using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Models;

namespace Nano35.Identity.Processor.UseCase.UpdatePassword
{
    public class UpdatePasswordConsumer : 
        IConsumer<IUpdatePasswordRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdatePasswordConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IUpdatePasswordRequestContract> context)
        {
            var userManager = (UserManager<User>) _services.GetService(typeof(UserManager<User>));
            var logger = (ILogger<IUpdatePasswordRequestContract>) _services.GetService(typeof(ILogger<IUpdatePasswordRequestContract>));
            var message = context.Message;
            var result = 
                await new LoggedPipeNode<IUpdatePasswordRequestContract, IUpdatePasswordResultContract>(logger,
                    new ValidatedUpdatePasswordRequest(
                        new UpdatePasswordUseCase(userManager))).Ask(message, context.CancellationToken);
            switch (result)
            {
                case IUpdatePasswordSuccessResultContract:
                    await context.RespondAsync<IUpdatePasswordSuccessResultContract>(result);
                    break;
                case IUpdatePasswordErrorResultContract:
                    await context.RespondAsync<IUpdatePasswordErrorResultContract>(result);
                    break;
            }
        }
    }
}