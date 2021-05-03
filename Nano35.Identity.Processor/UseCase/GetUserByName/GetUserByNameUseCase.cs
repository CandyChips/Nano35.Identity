using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.UseCase.GetUserByName
{
    public class GetUserByNameUseCase : UseCaseEndPointNodeBase<IGetUserByNameRequestContract, IGetUserByNameResultContract>
    {
        private readonly ApplicationContext _context;
        public GetUserByNameUseCase(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IGetUserByNameResultContract>> Ask(
            IGetUserByNameRequestContract request,
            CancellationToken cancellationToken)
        {
            var tmp = await _context
                .Users
                .FirstAsync(f => f.UserName == request.UserName, cancellationToken: cancellationToken);
            return tmp == null ?
                new UseCaseResponse<IGetUserByNameResultContract>("Пользователь не найден") :
                new UseCaseResponse<IGetUserByNameResultContract>(new GetUserByNameResultContract() { Data = 
                    new UserViewModel()
                    {
                        Email = tmp.Email,
                        Id = Guid.Parse(tmp.Id),
                        Name = tmp.Name,
                        Phone = tmp.PhoneNumber
                    }});
        }
    }
}