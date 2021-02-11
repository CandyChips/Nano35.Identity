using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Identity.Processor.Requests.Behaviours;
using Nano35.Identity.Processor.Services.Contexts;
using Nano35.Identity.Processor.Services.MappingProfiles;

namespace Nano35.Identity.Processor.Requests.GetAllRoles
{
    public class GetAllRolesRequest :
        IPipelineNode<IGetAllRolesRequestContract, IGetAllRolesResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllRolesRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetAllRolesSuccessResultContract : 
            IGetAllRolesSuccessResultContract
        {
            public IEnumerable<IRoleViewModel> Data { get; set; }
        }

        private class GetAllClientStatesErrorResultContract : 
            IGetAllRolesErrorResultContract
        {
            public string Message { get; set; }
        }

        public async Task<IGetAllRolesResultContract> Ask(
            IGetAllRolesRequestContract request,
            CancellationToken cancellationToken)
        {
            var result = await _context.Roles.MapAllToAsync<IRoleViewModel>();

            if (result.Count == 0)
                return new GetAllClientStatesErrorResultContract() {Message = "Не найдено ни одной записи"};
                
            return new GetAllRolesSuccessResultContract() {Data = result};
        }
    }
}