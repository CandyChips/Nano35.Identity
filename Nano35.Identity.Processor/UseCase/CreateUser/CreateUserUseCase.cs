using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Identity.Processor.Models;

namespace Nano35.Identity.Processor.UseCase.CreateUser
{
    public class CreateUserUseCase : UseCaseEndPointNodeBase<ICreateUserRequestContract, ICreateUserResultContract>
    {
        private readonly UserManager<User> _userManager;
        public CreateUserUseCase(UserManager<User> userManager) => _userManager = userManager;
        public override async Task<UseCaseResponse<ICreateUserResultContract>> Ask(
            ICreateUserRequestContract request,
            CancellationToken cancellationToken)
        {
            if (_userManager.Users.Select(a => a.Id).Contains(request.NewId.ToString()))
                return new UseCaseResponse<ICreateUserResultContract>("Повторите попытку");
            if(await _userManager.FindByNameAsync(request.Phone) != null)
                return new UseCaseResponse<ICreateUserResultContract>("Данный номер телефона уже существует в системе");
            if (await _userManager.FindByEmailAsync(request.Email) != null)
                return new UseCaseResponse<ICreateUserResultContract>("Данная электронная почта уже существует в системе");
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
                new UseCaseResponse<ICreateUserResultContract>("Пароли не совпадают") :
                new UseCaseResponse<ICreateUserResultContract>(new CreateUserResultContract());
        }
    }
}