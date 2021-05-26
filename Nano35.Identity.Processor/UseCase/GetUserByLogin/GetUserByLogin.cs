using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Identity.Processor.Models;
using Nano35.Identity.Processor.Services.Helpers;

namespace Nano35.Identity.Processor.UseCase.GetUserByLogin
{
    public class GetUserByLogin : EndPointNodeBase<IGetUserByLoginRequestContract, IGetUserByLoginResultContract>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtGenerator _jwtGenerator;
        public GetUserByLogin(UserManager<User> userManager, SignInManager<User> signInManager, IJwtGenerator jwtGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
        }
        public override async Task<UseCaseResponse<IGetUserByLoginResultContract>> Ask(
            IGetUserByLoginRequestContract request,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Login);
            if (user == null) return Pass("Пользователь не найден");
            var checkPasswordSignInAsyncResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!checkPasswordSignInAsyncResult.Succeeded)
                return Pass("Неверный пароль");
            var isEmailConfirmedAsyncResult = await _userManager.IsEmailConfirmedAsync(user);
            return !isEmailConfirmedAsyncResult ?
                Pass("Подтвердите почту") : 
                Pass(new GetUserByLoginResultContract() { User = new UserViewModel() { Id = Guid.Parse(user.Id), Email = user.Email, Name = user.Name, Phone = user.UserName }});
        }
    }
}