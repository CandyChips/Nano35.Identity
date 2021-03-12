using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Identity.Processor.Models;

namespace Nano35.Identity.Processor.UseCase.UpdateEmail
{
    public class UpdateEmailRequest :
        EndPointNodeBase<IUpdateEmailRequestContract, IUpdateEmailResultContract>
    {
        private readonly UserManager<User> _userManager;

        public UpdateEmailRequest(
            UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        private class UpdateEmailSuccessResultContract : 
            IUpdateEmailSuccessResultContract
        {
            public IEnumerable<IUserViewModel> Data { get; set; }
        }

        private class GetAllClientStatesErrorResultContract : 
            IUpdateEmailErrorResultContract
        {
            public string Message { get; set; }
        }

        public override Task<IUpdateEmailResultContract> Ask(
            IUpdateEmailRequestContract request,
            CancellationToken cancellationToken)
        {   
            throw new NotImplementedException();
        }
    }
}