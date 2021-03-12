using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Identity.Processor.Models;

namespace Nano35.Identity.Processor.UseCase.UpdatePassword
{
    public class UpdatePasswordRequest :
        EndPointNodeBase<IUpdatePasswordRequestContract, IUpdatePasswordResultContract>
    {
        private readonly UserManager<User> _userManager;

        public UpdatePasswordRequest(
            UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        
        private class UpdatePasswordSuccessResultContract : 
            IUpdatePasswordSuccessResultContract
        {
            public IEnumerable<IUserViewModel> Data { get; set; }
        }

        private class GetAllClientStatesErrorResultContract : 
            IUpdatePasswordErrorResultContract
        {
            public string Message { get; set; }
        }

        public override Task<IUpdatePasswordResultContract> Ask(
            IUpdatePasswordRequestContract request,
            CancellationToken cancellationToken)
        {   
            throw new NotImplementedException();
        }
    }
}