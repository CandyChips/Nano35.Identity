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
    public class GetRoleByUserIdQuery :
        IGetRoleByUserIdRequestContract,
        IQueryRequest<IGetRoleByUserIdResultContract>
    {
        public Guid UserId { get; set; }
        
        private class GetRoleByUserIdSuccessResultContract : 
            IGetRoleByUserIdSuccessResultContract
        {
            public IRoleViewModel Data { get; set; }
        }
        
        private class GetRoleByUserIdErrorResultContract : 
            IGetRoleByUserIdErrorResultContract
        {
            public string Message { get; set; }
        }
        
        public class GetRoleByUserIdHandler :
            IRequestHandler<GetRoleByUserIdQuery, IGetRoleByUserIdResultContract>
        {
            private readonly ApplicationContext _context;

            public GetRoleByUserIdHandler(ApplicationContext context)
            {
                _context = context;
            }


            public async Task<IGetRoleByUserIdResultContract> Handle(
                GetRoleByUserIdQuery request,
                CancellationToken cancellationToken)
            {
                // ToDo fix
                var result = (await _context.Roles.FirstOrDefaultAsync(f => f.Id == request.UserId.ToString(), cancellationToken: cancellationToken)).MapTo<IRoleViewModel>();

                if (result == null)
                    return new GetRoleByUserIdErrorResultContract() {Message = "Не найдено"};
                
                return new GetRoleByUserIdSuccessResultContract() {Data = result};
            }
        }

    }
}