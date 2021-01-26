using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Models;
using Nano35.Identity.Processor.Services.Helpers;

namespace Nano35.Identity.Processor.Services.MassTransit.Consumers
{
    public class GenerateTokenConsumer : 
        IConsumer<IGenerateTokenRequestContract>
    {
        private readonly ILogger<GenerateTokenConsumer> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtGenerator _jwtGenerator;

        public GenerateTokenConsumer(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IJwtGenerator jwtGenerator, 
            ILogger<GenerateTokenConsumer> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
            _logger = logger;
        }

        public async Task Consume(
            ConsumeContext<IGenerateTokenRequestContract> context)
        {
            _logger.LogInformation("IGenerateTokenRequestContract tracked");
            var message = context.Message;
            var user = await _userManager.FindByNameAsync(message.Login);
            if (user == null)
            {
                await context.RespondAsync<IGenerateTokenErrorResultContract>(new 
                {
                    Message = "Пользователь не найден"
                });
                return;
            }

            var checkPasswordSignInAsyncResult = await _signInManager.CheckPasswordSignInAsync(user, message.Password, false);
            if (!checkPasswordSignInAsyncResult.Succeeded)
            {
                await context.RespondAsync<IGenerateTokenErrorResultContract>(new 
                {
                    Message = "Неверный пароль"
                });
                return;
            }

            var isEmailConfirmedAsyncResult = await _userManager.IsEmailConfirmedAsync(user);
            if (!isEmailConfirmedAsyncResult)
            {
                await context.RespondAsync<IGenerateTokenErrorResultContract>(new 
                {
                    Message = "Подтвердите почту"
                });
                return;
            }
            await context.RespondAsync<IGenerateTokenSuccessResultContract>(new
            {
                Token = _jwtGenerator.CreateToken(user)
            });
        }
    }
}