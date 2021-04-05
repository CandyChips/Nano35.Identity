using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Models;

namespace Nano35.Identity.Processor.UseCase.GetAllUsers
{
    public class GetAllUsersConsumer : 
        IConsumer<IGetAllUsersRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public GetAllUsersConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(ConsumeContext<IGetAllUsersRequestContract> context)
        {
            var result = 
                await new LoggedPipeNode<IGetAllUsersRequestContract, IGetAllUsersResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllUsersRequestContract>)) as ILogger<IGetAllUsersRequestContract>,  
                    new GetAllUsersUseCase(_services.GetService(typeof(UserManager<User>)) as UserManager<User>)).Ask(context.Message, context.CancellationToken);
            switch (result)
            {
                case IGetAllUsersSuccessResultContract:
                    await context.RespondAsync<IGetAllUsersSuccessResultContract>(result);
                    break;
                case IGetAllUsersErrorResultContract:
                    await context.RespondAsync<IGetAllUsersErrorResultContract>(result);
                    break;
            }
        }
    }
}