using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Identity.Processor.Models;
using Nano35.Identity.Processor.Requests.Behaviours;
using Nano35.Identity.Processor.Services.Contexts;
using Nano35.Identity.Processor.Services.Helpers;
using Nano35.Identity.Processor.Services.MappingProfiles;

namespace Nano35.Identity.Processor.Requests
{
    public class GetRoleByIdQuery :
        IGetRoleByIdRequestContract,
        IQueryRequest<IGetRoleByIdResultContract>
    {
        public Guid RoleId { get; set; }
        
        private class GetRoleByIdSuccessResultContract : 
            IGetRoleByIdSuccessResultContract
        {
            public IRoleViewModel Data { get; set; }
        }
        
        private class GetRoleByIdErrorResultContract : 
            IGetRoleByIdErrorResultContract
        {
            public string Message { get; set; }
        }
        
        public class GetRoleByIdHandler :
            IRequestHandler<GetRoleByIdQuery, IGetRoleByIdResultContract>
        {
            private readonly ApplicationContext _context;

            public GetRoleByIdHandler(ApplicationContext context)
            {
                _context = context;
            }


            public async Task<IGetRoleByIdResultContract> Handle(
                GetRoleByIdQuery request,
                CancellationToken cancellationToken)
            {
                var result = (await _context.Roles.FirstOrDefaultAsync(f => f.Id == request.RoleId.ToString(), cancellationToken: cancellationToken)).MapTo<IRoleViewModel>();

                if (result == null)
                    return new GetRoleByIdErrorResultContract() {Message = "Не найдено"};
                
                return new GetRoleByIdSuccessResultContract() {Data = result};
            }
        }
    }
}