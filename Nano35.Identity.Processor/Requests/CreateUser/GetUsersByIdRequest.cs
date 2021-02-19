using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.Requests.CreateUser
{
    public class GetUserByRoleIdRequest :
        IPipelineNode<
            ICreateUserRequestContract,
            ICreateUserResultContract>
    {
        private readonly ApplicationContext _context;

        public GetUserByRoleIdRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetUserByRoleIdSuccessResultContract : 
            ICreateUserSuccessResultContract
        {
            public IEnumerable<IUserViewModel> Data { get; set; }
        }

        private class GetAllClientStatesErrorResultContract : 
            ICreateUserErrorResultContract
        {
            public string Error { get; set; }
        }

        public async Task<ICreateUserResultContract> Ask(
            ICreateUserRequestContract request,
            CancellationToken cancellationToken)
        {   
            throw new NotImplementedException();
        }
    }
}