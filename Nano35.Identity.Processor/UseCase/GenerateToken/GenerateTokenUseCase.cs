using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Identity.Processor.Models;
using Nano35.Identity.Processor.Services.Helpers;

namespace Nano35.Identity.Processor.UseCase.GenerateToken
{
    public class GenerateTokenUseCase : UseCaseEndPointNodeBase<IGenerateTokenRequestContract, IGenerateTokenResultContract>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtGenerator _jwtGenerator;
        public GenerateTokenUseCase(UserManager<User> userManager, SignInManager<User> signInManager, IJwtGenerator jwtGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
        }
        public override async Task<UseCaseResponse<IGenerateTokenResultContract>> Ask(
            IGenerateTokenRequestContract request,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Login);
            if (user == null)
                return new UseCaseResponse<IGenerateTokenResultContract>("Пользователь не найден");
            var checkPasswordSignInAsyncResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!checkPasswordSignInAsyncResult.Succeeded)
                return new UseCaseResponse<IGenerateTokenResultContract>("Неверный пароль");
            var isEmailConfirmedAsyncResult = await _userManager.IsEmailConfirmedAsync(user);
            return !isEmailConfirmedAsyncResult ?
                new UseCaseResponse<IGenerateTokenResultContract>("Подтвердите почту") : 
                new UseCaseResponse<IGenerateTokenResultContract>(new GenerateTokenResultContract() {Token = _jwtGenerator.CreateToken(user)});
        }
    }
}