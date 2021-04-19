using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Identity.Processor.Models;

namespace Nano35.Identity.Processor.UseCase.UpdatePhone
{
    public class UpdatePhoneUseCase :
        EndPointNodeBase<IUpdatePhoneRequestContract, IUpdatePhoneResultContract>
    {
        private readonly UserManager<User> _userManager;

        public UpdatePhoneUseCase(
            UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public override async Task<IUpdatePhoneResultContract> Ask(
            IUpdatePhoneRequestContract request,
            CancellationToken cancellationToken)
        {
            var result =
                await (_userManager.Users.FirstOrDefaultAsync(a => a.Id == request.UserId.ToString(),
                    cancellationToken));
            result.PhoneNumber = request.Phone;
            await _userManager.UpdateAsync(result);

            return new UpdatePhoneSuccessResultContract();
        }


    }
}