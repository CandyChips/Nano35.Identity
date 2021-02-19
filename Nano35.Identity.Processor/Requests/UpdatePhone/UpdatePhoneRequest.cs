using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.Requests.UpdatePhone
{
    public class GetUserByRoleIdRequest :
        IPipelineNode<IUpdatePhoneRequestContract, IUpdatePhoneResultContract>
    {
        public Guid UserId { get; set; }
        
        private readonly ApplicationContext _context;

        public GetUserByRoleIdRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetUserByRoleIdSuccessResultContract : 
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