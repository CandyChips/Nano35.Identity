using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.Requests.UpdatePassword
{
    public class GetUserByRoleIdRequest :
        IPipelineNode<IUpdatePasswordRequestContract, IUpdatePasswordResultContract>
    {
        public Guid UserId { get; set; }
        
        private readonly ApplicationContext _context;

        public GetUserByRoleIdRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetUserByRoleIdSuccessResultContract : 
            IUpdatePasswordSuccessResultContract
        {
            public IEnumerable<IUserViewModel> Data { get; set; }
        }

        private class GetAllClientStatesErrorResultContract : 
            IUpdatePasswordErrorResultContract
        {
            public string Error { get; set; }
        }

        public async Task<IUpdatePasswordResultContract> Ask(
            IUpdatePasswordRequestContract request,
            CancellationToken cancellationToken)
        {   
            throw new NotImplementedException();
        }
    }
}