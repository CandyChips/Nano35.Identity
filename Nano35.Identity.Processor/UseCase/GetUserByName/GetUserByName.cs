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

namespace Nano35.Identity.Processor.UseCase.GetUserByName
{
    public class GetUserByName : EndPointNodeBase<IGetUserByNameRequestContract, IGetUserByNameResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;
        public GetUserByName(ApplicationContext context, UserManager<User> userManager) { _context = context; _userManager = userManager; }
        public override async Task<UseCaseResponse<IGetUserByNameResultContract>> Ask(
            IGetUserByNameRequestContract request,
            CancellationToken cancellationToken)
        {
            var tmp = await _context
                .Users
                .FirstAsync(f => f.UserName == request.UserName, cancellationToken: cancellationToken);
            return tmp == null ?
                Pass("Пользователь не найден") :
                Pass(new GetUserByNameResultContract() { Data = 
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