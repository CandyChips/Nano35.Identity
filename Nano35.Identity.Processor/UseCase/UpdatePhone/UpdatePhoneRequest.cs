using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Identity.Processor.Models;

namespace Nano35.Identity.Processor.UseCase.UpdatePhone
{
    public class UpdatePhoneRequest :
        EndPointNodeBase<IUpdatePhoneRequestContract, IUpdatePhoneResultContract>
    {
        private readonly UserManager<User> _userManager;

        public UpdatePhoneRequest(
            UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        private class UpdatePhoneSuccessResultContract : 
            IUpdatePhoneSuccessResultContract
        {
            public IEnumerable<IUserViewModel> Data { get; set; }
        }

        private class GetAllClientStatesErrorResultContract : 
            IUpdatePhoneErrorResultContract
        {
            public string Message { get; set; }
        }

        public override Task<IUpdatePhoneResultContract> Ask(
            IUpdatePhoneRequestContract request,
            CancellationToken cancellationToken)
        {   
            throw new NotImplementedException();
        }
    }
}