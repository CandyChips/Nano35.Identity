using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.Requests.GetUsersByRoleId
{
    public class GetUserByRoleIdRequest :
        IPipelineNode<
            IGetUsersByRoleIdRequestContract,
            IGetUsersByRoleIdResultContract>
    {
        private readonly ApplicationContext _context;

        public GetUserByRoleIdRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetUserByRoleIdSuccessResultContract : 
            IGetUsersByRoleIdSuccessResultContract
        {
            public IEnumerable<IUserViewModel> Data { get; set; }
        }

        private class GetAllClientStatesErrorResultContract : 
            IGetUsersByRoleIdNotFoundResultContract
        {
            public string Message { get; set; }
        }

        public async Task<IGetUsersByRoleIdResultContract> Ask(
            IGetUsersByRoleIdRequestContract request,
            CancellationToken cancellationToken)
        {   
            throw new NotImplementedException();
        }
    }
}