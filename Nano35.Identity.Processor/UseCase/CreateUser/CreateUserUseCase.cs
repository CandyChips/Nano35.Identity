using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Identity.Processor.Models;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.UseCase.CreateUser
{
    public class CreateUserUseCase :
        EndPointNodeBase<
            ICreateUserRequestContract,
            ICreateUserResultContract>
    {
        private readonly UserManager<User> _userManager;


        public CreateUserUseCase(
            UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        
        private class CreateUserSuccessResultContract : 
            ICreateUserSuccessResultContract
        {
            public IEnumerable<IUserViewModel> Data { get; set; }
        }

        private class CreateUserErrorResultContract : 
            ICreateUserErrorResultContract
        {
            public string Message { get; set; }
        }

        public override async Task<ICreateUserResultContract> Ask(
            ICreateUserRequestContract request,
            CancellationToken cancellationToken)
        {

            if (_userManager.Users.Select(a => a.Id).Contains(request.NewId.ToString()))
            {
                return new CreateUserErrorResultContract() {Message = "Повторите попытку"};
            }
            var isUsersPhoneExist = await _userManager.FindByNameAsync(request.Phone);
            if (isUsersPhoneExist != null)
            {
                return new CreateUserErrorResultContract() {Message = "Данный номер телефона уже существует в системе"};
            }
            var isUsersEmailExist = await _userManager.FindByEmailAsync(request.Email);
            if (isUsersEmailExist != null)
            {
                return new CreateUserErrorResultContract() {Message = "Данная электронная почта уже существует в системе"};
            }
            var worker = new User()
            {
                Id = request.NewId.ToString(),
                UserName = request.Phone,
                Email = request.Email,
                Name = request.Name,
                Deleted = false,
                EmailConfirmed = true
            };
            var createAsyncResult = await _userManager.CreateAsync(worker, request.Password);
            if (!createAsyncResult.Succeeded)
            {
                return new CreateUserErrorResultContract() {Message = "Пароли не совпадают"};
            }
            
            return new CreateUserSuccessResultContract();
        }
    }
}