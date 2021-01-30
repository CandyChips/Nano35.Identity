using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Models;
using Nano35.Identity.Processor.Requests.Behaviours;
using Nano35.Identity.Processor.Services.Helpers;

namespace Nano35.Identity.Processor.Requests
{
    public class GenerateTokenQuery :
        IGenerateTokenRequestContract,
        IQueryRequest<IGenerateTokenResultContract>
    {
        public string Login { get; set; }
        public string Password { get; set; }
        
        private class GenerateTokenSuccessResultContract : 
            IGenerateTokenSuccessResultContract
        {
            public string Token { get; set; }
        }
        
        private class GenerateTokenErrorResultContract : 
            IGenerateTokenErrorResultContract
        {
            public string Message { get; set; }
        }
        
        public class GenerateTokenHandler :
            IRequestHandler<GenerateTokenQuery, IGenerateTokenResultContract>
        {
            private readonly UserManager<User> _userManager;
            private readonly SignInManager<User> _signInManager;
            private readonly IJwtGenerator _jwtGenerator;

            public GenerateTokenHandler(
                UserManager<User> userManager, 
                SignInManager<User> signInManager,
                IJwtGenerator jwtGenerator)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _jwtGenerator = jwtGenerator;
            }
            
            public async Task<IGenerateTokenResultContract> Handle(
                GenerateTokenQuery request,
                CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(request.Login);
                if (user == null)
                {
                    return new GenerateTokenErrorResultContract() {Message = "Пользователь не найден"};
                }

                var checkPasswordSignInAsyncResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
                if (!checkPasswordSignInAsyncResult.Succeeded)
                {
                    return new GenerateTokenErrorResultContract() {Message = "Неверный пароль"};
                }

                var isEmailConfirmedAsyncResult = await _userManager.IsEmailConfirmedAsync(user);
                if (!isEmailConfirmedAsyncResult)
                {
                    return new GenerateTokenErrorResultContract() {Message = "Подтвердите почту"};
                }

                return new GenerateTokenSuccessResultContract() {Token = _jwtGenerator.CreateToken(user)};
            }
        }
    }
}