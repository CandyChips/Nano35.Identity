using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Identity.Processor.Models;

namespace Nano35.Identity.Processor.UseCase.ConfirmEmailOfUser
{
    public class ConfirmEmailOfUser : EndPointNodeBase<IConfirmEmailOfUserRequestContract, IConfirmEmailOfUserResultContract>
    {
        private readonly UserManager<User> _userManager;
        public ConfirmEmailOfUser(UserManager<User> userManager) => _userManager = userManager;
        public override async Task<UseCaseResponse<IConfirmEmailOfUserResultContract>> Ask(
            IConfirmEmailOfUserRequestContract input, 
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(input.UserId.ToString());
            await _userManager.ConfirmEmailAsync(user, input.Key);
            return new UseCaseResponse<IConfirmEmailOfUserResultContract>(new ConfirmEmailOfUserResultContract());
        }
    }
}
