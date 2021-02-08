using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Identity.Processor.Requests.Behaviours;
using Nano35.Identity.Processor.Services.Contexts;
using Nano35.Identity.Processor.Services.MappingProfiles;

namespace Nano35.Identity.Processor.Requests.GetAllUsers
{
    public class GetAllUsersRequest :
        IGetAllUsersRequestContract,
        IQueryRequest<IGetAllUsersResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllUsersRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetAllUsersSuccessResultContract : 
            IGetAllUsersSuccessResultContract
        {
            public IEnumerable<IUserViewModel> Data { get; set; }
        }

        private class GetAllClientStatesErrorResultContract : 
            IGetAllUsersErrorResultContract
        {
            public string Message { get; set; }
        }

        public async Task<IGetAllUsersResultContract> Ask(
            GetAllUsersQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _context.Users.MapAllToAsync<IUserViewModel>();

            if (result.Count == 0)
                return new GetAllClientStatesErrorResultContract() {Message = "Не найдено ни одной записи"};
                
            return new GetAllUsersSuccessResultContract() {Data = result};
        }
    }
}