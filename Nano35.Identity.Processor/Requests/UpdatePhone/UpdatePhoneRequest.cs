using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Identity.Processor.Models;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.Requests.UpdatePhone
{
    public class UpdatePhoneRequest :
        IPipelineNode<
            IUpdatePhoneRequestContract,
            IUpdatePhoneResultContract>
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
            public string Error { get; set; }
        }

        public async Task<IUpdatePhoneResultContract> Ask(
            IUpdatePhoneRequestContract request,
            CancellationToken cancellationToken)
        {   
            throw new NotImplementedException();
        }
    }
}