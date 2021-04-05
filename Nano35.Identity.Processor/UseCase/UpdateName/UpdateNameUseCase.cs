using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Identity.Processor.Models;
using Nano35.Identity.Processor.UseCase.UpdatePassword;

namespace Nano35.Identity.Processor.UseCase.UpdateName
{
    public class UpdateNameUseCase :
        EndPointNodeBase<IUpdateNameRequestContract, IUpdateNameResultContract>
    {
        private readonly UserManager<User> _userManager;

        public UpdateNameUseCase(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public override async Task<IUpdateNameResultContract> Ask(
            IUpdateNameRequestContract request,
            CancellationToken cancellationToken)
        {
            var result =
                await (_userManager.Users.FirstOrDefaultAsync(a => Guid.Parse(a.Id) == request.UserId,
                    cancellationToken));
            result.Name = request.Name;

            return new UpdateNameSuccessResultContract();
        }
    }
}