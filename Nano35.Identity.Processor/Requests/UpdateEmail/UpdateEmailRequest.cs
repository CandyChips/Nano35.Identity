using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Identity.Processor.Models;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.Requests.UpdateEmail
{
    public class UpdateEmailRequest :
        IPipelineNode<
            IUpdateEmailRequestContract,
            IUpdateEmailResultContract>
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
            public string Error { get; set; }
        }

        public async Task<IUpdateEmailResultContract> Ask(
            IUpdateEmailRequestContract request,
            CancellationToken cancellationToken)
        {   
            throw new NotImplementedException();
        }
    }
}