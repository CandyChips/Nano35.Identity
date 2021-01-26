using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Identity.Processor.Models;
using Nano35.Identity.Processor.Services.MappingProfiles;

namespace Nano35.Identity.Processor.Services.MassTransit.Consumers
{
    public class GetUserByIdConsumer : 
        IConsumer<IGetUserByIdRequestContract>
    {
        private readonly ILogger<GetUserByIdConsumer> _logger;
        private readonly UserManager<User> _userManager;
        public GetUserByIdConsumer(
            UserManager<User> userManager, 
            ILogger<GetUserByIdConsumer> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<IGetUserByIdRequestContract> context)
        {
            _logger.LogInformation("IGetUserByIdRequestContract tracked");
            var result = (await this._userManager.FindByIdAsync(context.Message.UserId.ToString())).MapTo<IUserViewModel>();
            await context.RespondAsync<IGetUserByIdSuccessResultContract>(new
            {
                Data = result
            });
        }
    }
}