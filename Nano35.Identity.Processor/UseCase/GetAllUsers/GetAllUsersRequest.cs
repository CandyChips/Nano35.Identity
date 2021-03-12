﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Identity.Processor.Models;
using Nano35.Identity.Processor.Services.MappingProfiles;

namespace Nano35.Identity.Processor.UseCase.GetAllUsers
{
    public class GetAllUsersRequest :
        EndPointNodeBase<IGetAllUsersRequestContract, IGetAllUsersResultContract>
    {
        private readonly UserManager<User> _userManager;

        public GetAllUsersRequest(
            UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        
        private class GetAllUsersSuccessResultContract : 
            IGetAllUsersSuccessResultContract
        {
            public IEnumerable<IUserViewModel> Data { get; set; }
        }

        private class GetAllClientStatesErrorResultContract : 
            IGetAllUsersErrorResultContract
        {
            public string Message { get; set; }
        }

        public override async Task<IGetAllUsersResultContract> Ask(
            IGetAllUsersRequestContract request,
            CancellationToken cancellationToken)
        {
            var result = await _userManager.Users.MapAllToAsync<IUserViewModel>();
            
            if (result.Count == 0)
                return new GetAllClientStatesErrorResultContract() {Message = "Не найдено ни одной записи"};
                
            return new GetAllUsersSuccessResultContract() {Data = result};
        }
    }
}