using System.Threading;
using System.Threading.Tasks;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Identity.Processor.Services.Contexts;
using Nano35.Identity.Processor.Services.MappingProfiles;

namespace Nano35.Identity.Processor.UseCase.GetUserByName
{
    public class GetUserByNameUseCase :
        RailEndPointNodeBase<IGetUserByNameRequestContract, IGetUserByNameSuccessResultContract>
    {
        private readonly ApplicationContext _context;

        public GetUserByNameUseCase(ApplicationContext context)
        {
            _context = context;
        }

        public override async Task<Either<string, IGetUserByNameSuccessResultContract>> Ask(
            IGetUserByNameRequestContract request,
            CancellationToken cancellationToken)
        {
            var result = (await _context.Users.FirstOrDefaultAsync(f => f.UserName == request.UserName, cancellationToken: cancellationToken)).MapTo<IUserViewModel>();

            if (result == null) return "Не найдено";
                
            return new GetUserByNameSuccessResultContract() { Data = result };
        }
    }
}