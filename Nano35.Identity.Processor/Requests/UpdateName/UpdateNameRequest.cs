using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Identity.Processor.Models;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.Requests.UpdateName
{
    public class UpdateNameRequest :
        IPipelineNode<
            IUpdateNameRequestContract,
            IUpdateNameResultContract>
    {
        private readonly UserManager<User> _userManager;

        public UpdateNameRequest(
            UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        private class UpdateNameSuccessResultContract : 
            IUpdateNameSuccessResultContract
        {
            public IEnumerable<IUserViewModel> Data { get; set; }
        }

        private class GetAllClientStatesErrorResultContract : 
            IUpdateNameErrorResultContract
        {
            public string Error { get; set; }
        }

        public async Task<IUpdateNameResultContract> Ask(
            IUpdateNameRequestContract request,
            CancellationToken cancellationToken)
        {   
            throw new NotImplementedException();
        }
    }
}