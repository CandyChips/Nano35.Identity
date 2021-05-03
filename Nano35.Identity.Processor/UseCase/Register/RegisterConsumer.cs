using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Models;

namespace Nano35.Identity.Processor.UseCase.Register
{
    public class RegisterConsumer : IConsumer<IRegisterRequestContract>
    {
        private readonly IServiceProvider  _services;
        public RegisterConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IRegisterRequestContract> context)
        {
            var result = 
                await new LoggedUseCasePipeNode<IRegisterRequestContract, IRegisterResultContract>(
                    _services.GetService(typeof(ILogger<IRegisterRequestContract>)) as ILogger<IRegisterRequestContract>,
                    new RegisterUseCase(
                        _services.GetService(typeof(UserManager<User>)) as UserManager<User>))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}