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

namespace Nano35.Identity.Processor.Requests.GetUsersByRoleId
{
    public class GetUserByRoleIdRequest :
        IPipelineNode<IGetUsersByRoleIdRequestContract, IGetUsersByRoleIdResultContract>
    {
        public Guid UserId { get; set; }
        
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
            //var result = (await _context.Users.FirstOrDefaultAsync(f => f.Id == request.Id.ToString(), cancellationToken: cancellationToken)).MapTo<IEnumerable<IUserViewModel>>();
            throw new NotImplementedException();
            if (result == null)
                return new GetAllClientStatesErrorResultContract() {Message = "Не найдено"};
                
            return new GetUserByRoleIdSuccessResultContract() {Data = result};
        }
    }
}