using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Identity.Processor.Models;
using Nano35.Identity.Processor.Services.Helpers;

namespace Nano35.Identity.Processor.UseCase.GetUserClaimsByLogin
{
    public class GetUserClaimsByLogin : EndPointNodeBase<IGetUserClaimsByLoginRequestContract, IGetUserClaimsByLoginResultContract>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public GetUserClaimsByLogin(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public override async Task<UseCaseResponse<IGetUserClaimsByLoginResultContract>> Ask(
            IGetUserClaimsByLoginRequestContract request,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Login);
            if (user == null) return Pass("Пользователь не найден");
            var checkPasswordSignInAsyncResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!checkPasswordSignInAsyncResult.Succeeded)
                return Pass("Неверный пароль");
            var isEmailConfirmedAsyncResult = await _userManager.IsEmailConfirmedAsync(user);
            var principal = await _signInManager.CreateUserPrincipalAsync(user);
            return !isEmailConfirmedAsyncResult ?
                Pass("Подтвердите почту") : 
                Pass(new GetUserClaimsByLoginResultContract() { Principal = principal });
        }
    }
}