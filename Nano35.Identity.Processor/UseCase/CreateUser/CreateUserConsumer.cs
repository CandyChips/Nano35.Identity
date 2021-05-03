using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Models;

namespace Nano35.Identity.Processor.UseCase.CreateUser
{
    public class CreateUserConsumer : IConsumer<ICreateUserRequestContract>
    {
        private readonly IServiceProvider _services;
        public CreateUserConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<ICreateUserRequestContract> context)
        {
            var result = 
                await new LoggedUseCasePipeNode<ICreateUserRequestContract, ICreateUserResultContract>(
                    _services.GetService(typeof(ILogger<ICreateUserRequestContract>)) as ILogger<ICreateUserRequestContract>,  
                    new CreateUserUseCase(
                        _services.GetService(typeof(UserManager<User>)) as UserManager<User>))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}