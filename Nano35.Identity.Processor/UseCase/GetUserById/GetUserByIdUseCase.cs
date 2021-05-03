using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.UseCase.GetUserById
{
    public class GetUserByIdUseCase : UseCaseEndPointNodeBase<IGetUserByIdRequestContract, IGetUserByIdResultContract>
    {
        private readonly ApplicationContext _context;
        public GetUserByIdUseCase(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IGetUserByIdResultContract>> Ask(
            IGetUserByIdRequestContract request,
            CancellationToken cancellationToken)
        {
            var result = await _context.Users.FirstOrDefaultAsync(f => f.Id == request.UserId.ToString(), cancellationToken: cancellationToken);
            return result == null ?
                new UseCaseResponse<IGetUserByIdResultContract>("Пользователь не найден") :
                new UseCaseResponse<IGetUserByIdResultContract>(new GetUserByIdResultContract()
                {
                    Data =
                        new UserViewModel()
                        {
                            Email = result.Email,
                            Id = Guid.Parse(result.Id),
                            Name = result.Name,
                            Phone = result.PhoneNumber
                        }
                });
        }
    }
}