using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LanguageExt;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Identity.Processor.Models;

namespace Nano35.Identity.Processor.UseCase.UpdatePhone
{
    public class UpdatePhoneUseCase :
        RailEndPointNodeBase<IUpdatePhoneRequestContract, IUpdatePhoneSuccessResultContract>
    {
        private readonly UserManager<User> _userManager;

        public UpdatePhoneUseCase(
            UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public override async Task<Either<string, IUpdatePhoneSuccessResultContract>> Ask(
            IUpdatePhoneRequestContract request,
            CancellationToken cancellationToken)
        {
            var result = await (_userManager.Users.FirstOrDefaultAsync(a => Guid.Parse(a.Id) == request.UserId, cancellationToken));
            
            result.PhoneNumber = request.Phone;

            return new UpdatePhoneSuccessResultContract();
        }
    }
}