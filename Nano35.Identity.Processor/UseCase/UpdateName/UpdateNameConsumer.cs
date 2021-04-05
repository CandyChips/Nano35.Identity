using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Models;

namespace Nano35.Identity.Processor.UseCase.UpdateName
{
    public class UpdateNameConsumer : 
        IConsumer<IUpdateNameRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdateNameConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IUpdateNameRequestContract> context)
        {
            var userManager = (UserManager<User>) _services.GetService(typeof(UserManager<User>));
            var logger = (ILogger<IUpdateNameRequestContract>) _services.GetService(typeof(ILogger<IUpdateNameRequestContract>));
            var message = context.Message;
            var result = 
                await new LoggedPipeNode<IUpdateNameRequestContract, IUpdateNameResultContract>(logger,
                    new ValidatedUpdateNameRequest(
                        new UpdateNameUseCase(userManager))
                    ).Ask(message, context.CancellationToken);
            switch (result)
            {
                case IUpdateNameSuccessResultContract:
                    await context.RespondAsync<IUpdateNameSuccessResultContract>(result);
                    break;
                case IUpdateNameErrorResultContract:
                    await context.RespondAsync<IUpdateNameErrorResultContract>(result);
                    break;
            }
        }
    }
}