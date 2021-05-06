using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Identity.Processor.Models;

namespace Nano35.Identity.Processor.UseCase.Register
{
    public class Register : EndPointNodeBase<IRegisterRequestContract, IRegisterResultContract>
    {
        private readonly UserManager<User> _userManager;
        public Register(UserManager<User> userManager) => _userManager = userManager;
        public override async Task<UseCaseResponse<IRegisterResultContract>> Ask(
            IRegisterRequestContract input, 
            CancellationToken cancellationToken)
        {
            var isUsersPasswordCorrect = input.Password != input.PasswordConfirm;
            if (isUsersPasswordCorrect) return Pass("Пароли не совпадают");
            if (_userManager.Users.Select(a => a.Id).Contains(input.NewUserId.ToString())) return Pass("Повторите попытку");
            var isUsersPhoneExist = await _userManager.FindByNameAsync(input.Phone);
            if (isUsersPhoneExist != null) return Pass("Данный номер телефона уже существует в системе");
            var isUsersEmailExist = await _userManager.FindByEmailAsync(input.Email);
            if (isUsersEmailExist != null) return Pass("Данная электронная почта уже существует в системе");
            var worker =
                new User()
                    {Id = input.NewUserId.ToString(),
                     UserName = input.Phone,
                     Email = input.Email,
                     //Name = input.Name,
                     Deleted = false,
                     EmailConfirmed = true};
            var createAsyncResult = await _userManager.CreateAsync(worker, input.Password);
            return !createAsyncResult.Succeeded ?
                Pass("Пароли не совпадают") : 
                Pass(new RegisterResultContract());

            //var client = _bus.CreateRequestClient<IConfirmEmailOfUserRequestContract>(TimeSpan.FromSeconds(10));
            //var response = await client
            //    .GetResponse<IConfirmEmailOfUserResultContract, IConfirmEmailOfUserErrorResultContract>(
            //        new ConfirmEmailOfUserRequestContract()
            //        {
            //            Key = _userManager.GenerateChangeEmailTokenAsync(worker, worker.Email)
            //        });
            //if (response.Is(out Response<IConfirmEmailOfUserResultContract> Response))
            //if (response.Is(out Response<IConfirmEmailOfUserErrorResultContract> errorResponse))
            
            
            //var code = await UserManager.GenerateEmailConfirmationTokenAsync(TmpWorker);
            //var callbackUrl = Url.Action(
            //    "ConfirmEmail",
            //    "Account",
            //    new { userId = TmpWorker.Id, code = code },
            //    protocol: HttpContext.Request.Scheme);
            //await EmailService.SendEmailAsync(model.OrgEmail, "Confirm your account",
            //$"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");
            
            
        }
    }
}