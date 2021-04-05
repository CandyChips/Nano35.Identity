using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Identity.Processor.Models;

namespace Nano35.Identity.Processor.UseCase.UpdateEmail
{
    public class UpdateEmailUseCase :
        EndPointNodeBase<IUpdateEmailRequestContract, IUpdateEmailResultContract>
    {
        private readonly UserManager<User> _userManager;

        public UpdateEmailUseCase(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public override async Task<IUpdateEmailResultContract> Ask(
            IUpdateEmailRequestContract request,
            CancellationToken cancellationToken)
        {
            var result =
                await (_userManager.Users.FirstOrDefaultAsync(a => Guid.Parse(a.Id) == request.UserId,
                    cancellationToken));
            result.Email = request.Email;

            return new UpdateEmailSuccessResultContract();
        }
    }
}