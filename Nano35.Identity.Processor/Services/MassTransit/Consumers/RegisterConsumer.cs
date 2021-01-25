using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Consumers.Models;

namespace Nano35.Identity.Consumers.Services.MassTransit.Consumers
{
    public class RegisterConsumer : 
        IConsumer<IRegisterRequestContract>
    {
        private readonly ILogger<RegisterConsumer> _logger;
        private readonly UserManager<User> _userManager;
        public RegisterConsumer(
            UserManager<User> userManager, 
            ILogger<RegisterConsumer> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<IRegisterRequestContract> context)
        {
            _logger.LogInformation("IRegisterRequestContract tracked");
            var message = context.Message;
            var isUsersPasswordCorrect = message.Password != message.PasswordConfirm;
            if (isUsersPasswordCorrect)
            {
                await context.RespondAsync<IRegisterErrorResultContract>(new 
                {
                
                });
                return;
            }
            var isUsersPhoneExist = await _userManager.FindByNameAsync(message.Phone).ConfigureAwait(false);
            if (isUsersPhoneExist != null)
            {
                await context.RespondAsync<IRegisterErrorResultContract>(new 
                {
                
                });
                return;
            }
            var isUsersEmailExist = await _userManager.FindByEmailAsync(message.Email).ConfigureAwait(false);
            if (isUsersEmailExist != null)
            {
                await context.RespondAsync<IRegisterErrorResultContract>(new 
                {
                
                });
                return;
            }
            var worker = new User()
            {
                UserName = message.Phone,
                Email = message.Email,
                Name = "Оператор системы",
                Deleted = false,
                EmailConfirmed = true
            };
            var createAsyncResult = await _userManager.CreateAsync(worker, message.Password).ConfigureAwait(false);
            if (!createAsyncResult.Succeeded)
            {
                await context.RespondAsync<IRegisterErrorResultContract>(new 
                {
                
                });
                return;
            }
            /*var code = await UserManager.GenerateEmailConfirmationTokenAsync(TmpWorker);
            var callbackUrl = Url.Action(
                "ConfirmEmail",
                "Account",
                new { userId = TmpWorker.Id, code = code },
                protocol: HttpContext.Request.Scheme);
            await EmailService.SendEmailAsync(model.OrgEmail, "Confirm your account",
            $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");*/
            await context.RespondAsync<IRegisterSuccessResultContract>(new 
            {
                UserId = worker.Id 
            });
        }
    }
}