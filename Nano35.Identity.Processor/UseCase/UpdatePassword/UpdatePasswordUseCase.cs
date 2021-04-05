using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Identity.Processor.Models;

namespace Nano35.Identity.Processor.UseCase.UpdatePassword
{
    public class UpdatePasswordUseCase :
        EndPointNodeBase<IUpdatePasswordRequestContract, IUpdatePasswordResultContract>
    {
        private readonly UserManager<User> _userManager;

        public UpdatePasswordUseCase(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        
        public override async Task<IUpdatePasswordResultContract> Ask(
            IUpdatePasswordRequestContract request,
            CancellationToken cancellationToken)
        {
            var result =
                await (_userManager.Users.FirstOrDefaultAsync(a => Guid.Parse(a.Id) == request.UserId,
                    cancellationToken));
            var user = _userManager.FindByIdAsync(request.UserId.ToString()).Result;
            result.PasswordHash = _userManager.PasswordHasher.HashPassword(user ,request.Password);

            return new UpdatePasswordSuccessResultContract();
        }
    }
}