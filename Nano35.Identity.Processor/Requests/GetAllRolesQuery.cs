using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
    public class GetAllRolesQuery :
        IGetAllRolesRequestContract,
        IQueryRequest<IGetAllRolesResultContract>
    {
        private class GetAllRolesSuccessResultContract : 
            IGetAllRolesSuccessResultContract
        {
            public IEnumerable<IRoleViewModel> Data { get; set; }
        }
        
        private class GetAllRolesErrorResultContract : 
            IGetAllRolesErrorResultContract
        {
            public string Message { get; set; }
        }
        
        public class GetAllRolesHandler :
            IRequestHandler<GetAllRolesQuery, IGetAllRolesResultContract>
        {
            private readonly ApplicationContext _context;

            public GetAllRolesHandler(ApplicationContext context)
            {
                _context = context;
            }


            public async Task<IGetAllRolesResultContract> Handle(
                GetAllRolesQuery request,
                CancellationToken cancellationToken)
            {
                var result = await _context.Roles.MapAllToAsync<IRoleViewModel>();

                if (result.Count == 0)
                    return new GetAllRolesErrorResultContract() {Message = "Не найдено ни одной записи"};
                
                return new GetAllRolesSuccessResultContract() {Data = result};
            }
        }
    }
}