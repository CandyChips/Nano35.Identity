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
    public class GetRoleByIdConsumer : 
        IConsumer<IGetRoleByIdRequestContract>
    {
        private readonly ILogger<GetRoleByIdConsumer> _logger;
        private readonly UserManager<User> _userManager;
        public GetRoleByIdConsumer(
            UserManager<User> userManager, 
            ILogger<GetRoleByIdConsumer> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<IGetRoleByIdRequestContract> context)
        {
            _logger.LogInformation("IGetRoleByIdRequestContract tracked");
            var result = (await this._userManager.FindByIdAsync(context.Message.RoleId.ToString())).MapTo<IRoleViewModel>();
            await context.RespondAsync<IGetRoleByIdSuccessResultContract>(new
            {
                Data = result
            });
        }
    }
}