using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Identity.Processor.Services.Contexts;
using Nano35.Identity.Processor.Services.MappingProfiles;

namespace Nano35.Identity.Processor.UseCase.GetUserById
{
    public class GetUserByIdUseCase :
        EndPointNodeBase<IGetUserByIdRequestContract, IGetUserByIdResultContract>
    {
        private readonly ApplicationContext _context;

        public GetUserByIdUseCase(
            ApplicationContext context)
        {
            _context = context;
        }

        public override async Task<IGetUserByIdResultContract> Ask(
            IGetUserByIdRequestContract request,
            CancellationToken cancellationToken)
        {
            var result = (await _context.Users.FirstOrDefaultAsync(f => f.Id == request.UserId.ToString(), cancellationToken: cancellationToken)).MapTo<IUserViewModel>();

            if (result == null)
                return new GetUserByIdErrorResultContract() {Message = "Не найдено"};
                
            return new GetUserByIdSuccessResultContract() {Data = result};
        }
    }
}