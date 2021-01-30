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
    public class GetAllUsersQuery :
        IGetAllUsersRequestContract,
        IQueryRequest<IGetAllUsersResultContract>
    {
        private class GetAllUsersSuccessResultContract : 
            IGetAllUsersSuccessResultContract
        {
            public IEnumerable<IUserViewModel> Data { get; set; }
        }
        
        private class GetAllUsersErrorResultContract : 
            IGetAllUsersErrorResultContract
        {
            public string Message { get; set; }
        }
        
        public class GetAllUsersHandler :
            IRequestHandler<GetAllUsersQuery, IGetAllUsersResultContract>
        {
            private readonly ApplicationContext _context;

            public GetAllUsersHandler(ApplicationContext context)
            {
                _context = context;
            }


            public async Task<IGetAllUsersResultContract> Handle(
                GetAllUsersQuery request,
                CancellationToken cancellationToken)
            {
                var result = await _context.Users.MapAllToAsync<IUserViewModel>();

                if (result.Count == 0)
                    return new GetAllUsersErrorResultContract() {Message = "Не найдено ни одной записи"};
                
                return new GetAllUsersSuccessResultContract() {Data = result};
            }
        }
    }
}