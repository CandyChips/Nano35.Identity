using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.UseCase.CreateUser
{
    public class GetUserByRoleIdRequest :
        EndPointNodeBase<ICreateUserRequestContract, ICreateUserResultContract>
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
            public string Message { get; set; }
        }

        public override async Task<ICreateUserResultContract> Ask(
            ICreateUserRequestContract request,
            CancellationToken cancellationToken)
        {   
            throw new NotImplementedException();
        }
    }
}