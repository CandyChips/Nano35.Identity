using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Identity.Processor.Models;

namespace Nano35.Identity.Processor.UseCase.UpdatePassword
{
    public class UpdatePassword : EndPointNodeBase<IUpdatePasswordRequestContract, IUpdatePasswordResultContract>
    {
        public override async Task<UseCaseResponse<IUpdatePasswordResultContract>> Ask(
            IUpdatePasswordRequestContract request,
            CancellationToken cancellationToken)
        {                
            return Pass("Не работает в текущей версии");

        }
    }
}