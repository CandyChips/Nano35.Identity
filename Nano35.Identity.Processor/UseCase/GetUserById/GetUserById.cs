using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Identity.Processor.Models;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.UseCase.GetUserById
{
    public class GetUserById : EndPointNodeBase<IGetUserByIdRequestContract, IGetUserByIdResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;
        public GetUserById(ApplicationContext context, UserManager<User> userManager) { _context = context; _userManager = userManager; }
        public override async Task<UseCaseResponse<IGetUserByIdResultContract>> Ask(
            IGetUserByIdRequestContract request,
            CancellationToken cancellationToken)
        {
            var result = await _context.Users.FirstOrDefaultAsync(f => f.Id == request.UserId.ToString(), cancellationToken: cancellationToken);
            return result == null ?
                Pass("Пользователь не найден") :
                Pass(new GetUserByIdResultContract()
                {
                    Data =
                        new UserViewModel()
                        {
                            Email = result.Email,
                            Id = Guid.Parse(result.Id),
                            Name = result.Name,
                            Phone = result.UserName
                        }
                });
        }
    }
}