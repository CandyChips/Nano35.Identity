using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GetAllRoles
{
    public class GetAllRolesRequest :
        IPipelineNode<IGetAllRolesRequestContract, IGetAllRolesResultContract>
    {
        private readonly ILogger<GetAllRolesRequest> _logger;
        private readonly IBus _bus;

        public GetAllRolesRequest(
            IBus bus, 
            ILogger<GetAllRolesRequest> logger)
        {
            _bus = bus;
            _logger = logger;
        }
        
        public async Task<IGetAllRolesResultContract> Ask(
            IGetAllRolesRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetAllRolesRequestContract>(TimeSpan.FromSeconds(10));
                
            var response = await client
                .GetResponse<IGetAllRolesResultContract, IGetAllRolesErrorResultContract>(input);

            if (response.Is(out Response<IGetAllRolesResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetAllRolesErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}
