using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LanguageExt;
using Microsoft.AspNetCore.Identity;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Identity.Processor.Models;

namespace Nano35.Identity.Processor.UseCase.Register
{
    public class RegisterUseCase : 
        RailEndPointNodeBase<IRegisterRequestContract, IRegisterSuccessResultContract>
    {
        private readonly UserManager<User> _userManager;
        
        public RegisterUseCase(UserManager<User> userManager) { _userManager = userManager; }
        
        public override async Task<Either<string, IRegisterSuccessResultContract>> Ask(
            IRegisterRequestContract input, 
            CancellationToken cancellationToken)
        {
            var isUsersPasswordCorrect = input.Password != input.PasswordConfirm;
            if (isUsersPasswordCorrect)
            {
                return "Пароли не совпадают";
            }

            if (_userManager.Users.Select(a => a.Id).Contains(input.NewUserId.ToString()))
            {
                return "Повторите попытку";
            }
            var isUsersPhoneExist = await _userManager.FindByNameAsync(input.Phone);
            if (isUsersPhoneExist != null)
            {
                return "Данный номер телефона уже существует в системе";
            }
            var isUsersEmailExist = await _userManager.FindByEmailAsync(input.Email);
            if (isUsersEmailExist != null)
            {
                return "Данная электронная почта уже существует в системе";
            }
            var worker = new User()
            {
                Id = input.NewUserId.ToString(),
                UserName = input.Phone,
                Email = input.Email,
                Name = "Оператор системы",
                Deleted = false,
                EmailConfirmed = true
            };
            var createAsyncResult = await _userManager.CreateAsync(worker, input.Password);
            if (!createAsyncResult.Succeeded)
            {
                return "Пароли не совпадают";
            }
            

            //var client = _bus.CreateRequestClient<IConfirmEmailOfUserRequestContract>(TimeSpan.FromSeconds(10));
            //var response = await client
            //    .GetResponse<IConfirmEmailOfUserSuccessResultContract, IConfirmEmailOfUserErrorResultContract>(
            //        new ConfirmEmailOfUserRequestContract()
            //        {
            //            Key = _userManager.GenerateChangeEmailTokenAsync(worker, worker.Email)
            //        });
            //if (response.Is(out Response<IConfirmEmailOfUserSuccessResultContract> successResponse))
            //if (response.Is(out Response<IConfirmEmailOfUserErrorResultContract> errorResponse))
            
            
            //var code = await UserManager.GenerateEmailConfirmationTokenAsync(TmpWorker);
            //var callbackUrl = Url.Action(
            //    "ConfirmEmail",
            //    "Account",
            //    new { userId = TmpWorker.Id, code = code },
            //    protocol: HttpContext.Request.Scheme);
            //await EmailService.SendEmailAsync(model.OrgEmail, "Confirm your account",
            //$"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");
            
            
            return new RegisterSuccessResultContract();
        }
    }
}