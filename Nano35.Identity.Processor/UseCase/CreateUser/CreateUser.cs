using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Identity.Processor.Models;

namespace Nano35.Identity.Processor.UseCase.CreateUser
{
    public class CreateUser : EndPointNodeBase<ICreateUserRequestContract, ICreateUserResultContract>
    {
        private readonly UserManager<User> _userManager;
        public CreateUser(UserManager<User> userManager) => _userManager = userManager;
        public override async Task<UseCaseResponse<ICreateUserResultContract>> Ask(
            ICreateUserRequestContract request,
            CancellationToken cancellationToken)
        {
            if (_userManager.Users.Select(a => a.Id).Contains(request.NewId.ToString())) return Pass("Повторите попытку");
            if(await _userManager.FindByNameAsync(request.Phone) != null) return Pass("Данный номер телефона уже существует в системе");
            if (await _userManager.FindByEmailAsync(request.Email) != null) return Pass("Данная электронная почта уже существует в системе");
            var worker = new User()
            {
                Id = request.NewId.ToString(),
                UserName = request.Phone,
                Email = request.Email,
                Name = request.Name,
                Deleted = false,
                EmailConfirmed = true
            };
            return !(await _userManager.CreateAsync(worker, request.Password)).Succeeded ? 
                Pass("Ошибка при создании") :
                Pass(new CreateUserResultContract());
        }
    }
}