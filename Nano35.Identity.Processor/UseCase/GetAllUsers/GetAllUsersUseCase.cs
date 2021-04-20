using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LanguageExt;
using Microsoft.AspNetCore.Identity;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Identity.Processor.Models;
using Nano35.Identity.Processor.Services.MappingProfiles;

namespace Nano35.Identity.Processor.UseCase.GetAllUsers
{
    public class GetAllUsersUseCase :
        RailEndPointNodeBase<IGetAllUsersRequestContract, IGetAllUsersSuccessResultContract>
    {
        private readonly UserManager<User> _userManager;

        public GetAllUsersUseCase(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public override async Task<Either<string, IGetAllUsersSuccessResultContract>> Ask(
            IGetAllUsersRequestContract request,
            CancellationToken cancellationToken)
        {
            var result = await _userManager.Users.MapAllToAsync<IUserViewModel>();
            
            if (result.Count == 0) return "Не найдено ни одной записи";
                
            return new GetAllUsersSuccessResultContract() {Data = result};
        }
    }
}