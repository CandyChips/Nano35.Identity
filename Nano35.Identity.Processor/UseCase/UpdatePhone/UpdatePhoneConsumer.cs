using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Models;

namespace Nano35.Identity.Processor.UseCase.UpdatePhone
{
    public class UpdatePhoneConsumer : 
        IConsumer<IUpdatePhoneRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdatePhoneConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IUpdatePhoneRequestContract> context)
        {
            var userManager = (UserManager<User>) _services.GetService(typeof(UserManager<User>));
            var logger = (ILogger<IUpdatePhoneRequestContract>) _services.GetService(typeof(ILogger<IUpdatePhoneRequestContract>));
            var message = context.Message;
            var result = 
                await new LoggedPipeNode<IUpdatePhoneRequestContract, IUpdatePhoneResultContract>(logger,
                    new ValidatedUpdatePhoneRequest(
                        new UpdatePhoneUseCase(userManager))
                    ).Ask(message, context.CancellationToken);
            switch (result)
            {
                case IUpdatePhoneSuccessResultContract:
                    await context.RespondAsync<IUpdatePhoneSuccessResultContract>(result);
                    break;
                case IUpdatePhoneErrorResultContract:
                    await context.RespondAsync<IUpdatePhoneErrorResultContract>(result);
                    break;
            }
        }
    }
}