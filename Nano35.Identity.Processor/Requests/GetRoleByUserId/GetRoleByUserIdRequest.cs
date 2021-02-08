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

namespace Nano35.Identity.Processor.Requests.GetRoleByUserId
{
    public class GetRoleByUserIdRequest :
        IGetRoleByUserIdRequestContract,
        IQueryRequest<IGetRoleByUserIdResultContract>
    {
        public Guid UserId { get; set; }
        
        private readonly ApplicationContext _context;

        public GetRoleByUserIdRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetRoleByUserIdSuccessResultContract : 
            IGetRoleByUserIdSuccessResultContract
        {
            public IRoleViewModel Data { get; set; }
        }

        private class GetAllClientStatesErrorResultContract : 
            IGetRoleByUserIdErrorResultContract
        {
            public string Message { get; set; }
        }

        public async Task<IGetRoleByUserIdResultContract> Ask(
            GetRoleByUserIdQuery request,
            CancellationToken cancellationToken)
        {
            // ToDo fix
            var result = (await _context.Roles.FirstOrDefaultAsync(f => f.Id == request.UserId.ToString(), cancellationToken: cancellationToken)).MapTo<IRoleViewModel>();

            if (result == null)
                return new GetAllClientStatesErrorResultContract() {Message = "Не найдено"};
                
            return new GetRoleByUserIdSuccessResultContract() {Data = result};
        }
    }
}