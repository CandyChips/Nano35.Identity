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

namespace Nano35.Identity.Processor.Requests.GetRoleById
{
    public class GetRoleByIdRequest :
        IPipelineNode<IGetRoleByIdRequestContract, IGetRoleByIdResultContract>
    {
        public Guid RoleId { get; set; }
        
        private readonly ApplicationContext _context;

        public GetRoleByIdRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetRoleByIdSuccessResultContract : 
            IGetRoleByIdSuccessResultContract
        {
            public IRoleViewModel Data { get; set; }
        }

        private class GetAllClientStatesErrorResultContract : 
            IGetRoleByIdErrorResultContract
        {
            public string Message { get; set; }
        }

        public async Task<IGetRoleByIdResultContract> Ask(
            IGetRoleByIdRequestContract request,
            CancellationToken cancellationToken)
        {
            var result = (await _context.Roles.FirstOrDefaultAsync(f => f.Id == request.RoleId.ToString(), cancellationToken: cancellationToken)).MapTo<IRoleViewModel>();

            if (result == null)
                return new GetAllClientStatesErrorResultContract() {Message = "Не найдено"};
                
            return new GetRoleByIdSuccessResultContract() {Data = result};
        }

        
    }
}