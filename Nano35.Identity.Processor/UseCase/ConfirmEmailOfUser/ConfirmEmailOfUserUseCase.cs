using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Models;

namespace Nano35.Identity.Processor.UseCase.ConfirmEmailOfUser
{
    public class ConfirmEmailOfUserUseCase :
        EndPointNodeBase<
            IConfirmEmailOfUserRequestContract,
            IConfirmEmailOfUserResultContract>
    {
        private readonly UserManager<User> _userManager;

        public ConfirmEmailOfUserUseCase(
            UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        
        private class ConfirmEmailOfUserSuccessResultContract : 
            IConfirmEmailOfUserSuccessResultContract
        {
        }

        public override async Task<IConfirmEmailOfUserResultContract> Ask(
            IConfirmEmailOfUserRequestContract input, 
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(input.UserId.ToString());
            await _userManager.ConfirmEmailAsync(user, input.Key);
            return new ConfirmEmailOfUserSuccessResultContract();
        }
    }
}
