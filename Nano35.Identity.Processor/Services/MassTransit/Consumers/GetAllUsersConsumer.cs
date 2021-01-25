using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Identity.Consumers.Models;
using Nano35.Identity.Consumers.Services.MappingProfiles;

namespace Nano35.Identity.Consumers.Services.MassTransit.Consumers
{
    public class GetAllUsersConsumer : 
        IConsumer<IGetAllUsersRequestContract>
    {
        private readonly ILogger<GetAllUsersConsumer> _logger;
        private readonly UserManager<User> _userManager;
        public GetAllUsersConsumer(
            UserManager<User> userManager, 
            ILogger<GetAllUsersConsumer> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<IGetAllUsersRequestContract> context)
        {
            _logger.LogInformation("IGetAllUsersRequestContract tracked");
            var result = await this._userManager.Users.MapAllToAsync<IUserViewModel>();
            await context.RespondAsync<IGetAllUsersSuccessResultContract>(new
            {
                Data = result
            });
        }
    }
}