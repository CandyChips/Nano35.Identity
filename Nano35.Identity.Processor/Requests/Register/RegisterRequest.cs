using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Identity.Processor.Models;
using Nano35.Identity.Processor.Requests.Behaviours;
using Nano35.Identity.Processor.Services.Contexts;
using Nano35.Identity.Processor.Services.MappingProfiles;

namespace Nano35.Identity.Processor.Requests.Register
{
    public class RegisterRequest :
        IPipelineNode<IRegisterRequestContract, IRegisterResultContract>
    {
        public Guid NewUserId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        
        private readonly UserManager<User> _userManager;

        public RegisterRequest(
            UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        
        private class RegisterSuccessResultContract : 
            IRegisterSuccessResultContract
        {
            public IUserViewModel Data { get; set; }
        }

        private class RegisterErrorResultContract : 
            IRegisterErrorResultContract
        {
            public string Error { get; set; }
        }

        public async Task<IRegisterResultContract> Ask(
            IRegisterRequestContract request, 
            CancellationToken cancellationToken)
            {
                var isUsersPasswordCorrect = request.Password != request.PasswordConfirm;
                if (isUsersPasswordCorrect)
                {
                    return new RegisterErrorResultContract() {Error = "Пароли не совпадают"};
                }

                if (_userManager.Users.Select(a => a.Id).Contains(request.NewUserId.ToString()))
                {
                    return new RegisterErrorResultContract() {Error = "Повторите попытку"};
                }
                var isUsersPhoneExist = await _userManager.FindByNameAsync(request.Phone);
                if (isUsersPhoneExist != null)
                {
                    return new RegisterErrorResultContract() {Error = "Данный номер телефона уже существует в системе"};
                }
                var isUsersEmailExist = await _userManager.FindByEmailAsync(request.Email);
                if (isUsersEmailExist != null)
                {
                    return new RegisterErrorResultContract() {Error = "Данная электронная почта уже существует в системе"};
                }
                var worker = new User()
                {
                    Id = request.NewUserId.ToString(),
                    UserName = request.Phone,
                    Email = request.Email,
                    Name = "Оператор системы",
                    Deleted = false,
                    EmailConfirmed = true
                };
                var createAsyncResult = await _userManager.CreateAsync(worker, request.Password);
                if (!createAsyncResult.Succeeded)
                {
                    return new RegisterErrorResultContract() {Error = "Пароли не совпадают"};
                }
                /*var code = await UserManager.GenerateEmailConfirmationTokenAsync(TmpWorker);
                var callbackUrl = Url.Action(
                    "ConfirmEmail",
                    "Account",
                    new { userId = TmpWorker.Id, code = code },
                    protocol: HttpContext.Request.Scheme);
                await EmailService.SendEmailAsync(model.OrgEmail, "Confirm your account",
                $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");*/
                return new RegisterSuccessResultContract();
            }
    }
}