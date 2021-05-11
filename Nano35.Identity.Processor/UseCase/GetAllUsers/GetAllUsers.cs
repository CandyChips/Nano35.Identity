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

namespace Nano35.Identity.Processor.UseCase.GetAllUsers
{
    public class GetAllUsers : EndPointNodeBase<IGetAllUsersRequestContract, IGetAllUsersResultContract>
    {
        private readonly UserManager<User> _userManager;
        public GetAllUsers(UserManager<User> userManager) => _userManager = userManager;
        public override async Task<UseCaseResponse<IGetAllUsersResultContract>> Ask(
            IGetAllUsersRequestContract request,
            CancellationToken cancellationToken)
        {
            var result = await _userManager
                .Users
                .Select(a =>
                    new UserViewModel()
                        {Email = a.Email,
                         Id = Guid.Parse(a.Id),
                         Name = a.Name,
                         Phone = a.UserName})
                .ToListAsync(cancellationToken);
            
            return result.Count == 0 ?
                Pass("Не найдено ни одной записи") : 
                Pass(new GetAllUsersResultContract() {Users = result});
        }
    }
}