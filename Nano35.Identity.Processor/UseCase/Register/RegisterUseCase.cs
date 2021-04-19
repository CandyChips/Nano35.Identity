﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Identity.Processor.Models;

namespace Nano35.Identity.Processor.UseCase.Register
{
    public class RegisterUseCase :
        EndPointNodeBase<IRegisterRequestContract, IRegisterResultContract>
    {
        private readonly UserManager<User> _userManager;

        public RegisterUseCase(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public override async Task<IRegisterResultContract> Ask(
            IRegisterRequestContract request,
            CancellationToken cancellationToken)
        {
            var isUsersPasswordCorrect = request.Password != request.PasswordConfirm;
            if (isUsersPasswordCorrect)
            {
                return new RegisterErrorResultContract() {Message = "Пароли не совпадают"};
            }

            if (_userManager.Users.Select(a => a.Id).Contains(request.NewUserId.ToString()))
            {
                return new RegisterErrorResultContract() {Message = "Повторите попытку"};
            }

            var isUsersPhoneExist = await _userManager.FindByNameAsync(request.Phone);
            if (isUsersPhoneExist != null)
            {
                return new RegisterErrorResultContract() {Message = "Данный номер телефона уже существует в системе"};
            }

            var isUsersEmailExist = await _userManager.FindByEmailAsync(request.Email);
            if (isUsersEmailExist != null)
            {
                return new RegisterErrorResultContract()
                    {Message = "Данная электронная почта уже существует в системе"};
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
                    return new RegisterErrorResultContract() {Message = "Пароли не совпадают"};
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