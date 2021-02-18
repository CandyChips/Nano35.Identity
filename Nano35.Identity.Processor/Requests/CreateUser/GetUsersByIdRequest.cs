using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Identity.Processor.Requests.Behaviours;
using Nano35.Identity.Processor.Services.Contexts;
using Nano35.Identity.Processor.Services.MappingProfiles;

namespace Nano35.Identity.Processor.Requests.CreateUser
{
    public class GetUserByRoleIdRequest :
        IPipelineNode<ICreateUserRequestContract, ICreateUserResultContract>
    {
        public Guid UserId { get; set; }
        
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
            if (result == null)
                return new GetAllClientStatesErrorResultContract() {Error = "Не найдено"};
                
            return new GetUserByRoleIdSuccessResultContract() {Data = result};
        }
    }
}