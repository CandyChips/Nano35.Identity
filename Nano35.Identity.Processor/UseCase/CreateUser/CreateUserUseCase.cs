using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LanguageExt;
using Microsoft.AspNetCore.Identity;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Identity.Processor.Models;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.UseCase.CreateUser
{
    public class CreateUserUseCase :
        RailEndPointNodeBase<ICreateUserRequestContract, ICreateUserSuccessResultContract>
    {
        private readonly UserManager<User> _userManager;
        
        public CreateUserUseCase(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public override async Task<Either<string, ICreateUserSuccessResultContract>> Ask(
            ICreateUserRequestContract request,
            CancellationToken cancellationToken)
        {
            if (_userManager.Users.Select(a => a.Id).Contains(request.NewId.ToString()))
                return "Повторите попытку";
            if(await _userManager.FindByNameAsync(request.Phone) != null)
                return "Данный номер телефона уже существует в системе";
            if (await _userManager.FindByEmailAsync(request.Email) != null)
                return "Данная электронная почта уже существует в системе";
            var worker = new User()
            {
                Id = request.NewId.ToString(),
                UserName = request.Phone,
                Email = request.Email,
                Name = request.Name,
                Deleted = false,
                EmailConfirmed = true
            };
            if (!(await _userManager.CreateAsync(worker, request.Password)).Succeeded)
                return "Пароли не совпадают";
            return new CreateUserSuccessResultContract();
        }
    }
}