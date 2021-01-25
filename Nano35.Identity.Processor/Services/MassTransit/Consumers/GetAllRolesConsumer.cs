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
    public class GetAllRolesConsumer : 
        IConsumer<IGetAllRolesRequestContract>
    {
        private readonly ILogger<GetAllRolesConsumer> _logger;
        private readonly UserManager<User> _userManager;
        public GetAllRolesConsumer(
            UserManager<User> userManager, 
            ILogger<GetAllRolesConsumer> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<IGetAllRolesRequestContract> context)
        {
            _logger.LogInformation("IGetAllUsersRequestContract tracked");
            var result = await this._userManager.Users.MapAllToAsync<IRoleViewModel>();
            await context.RespondAsync<IGetAllRolesResultContract>(new
            {
                Data = result
            });
        }
    }
}