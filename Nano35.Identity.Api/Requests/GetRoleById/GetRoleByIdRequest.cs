using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GetRoleById
{
    public class GetRoleByIdRequest :
        IPipelineNode<IGetRoleByIdRequestContract, IGetRoleByIdResultContract>
    {
        public Guid RoleId { get; set; }
        
        private readonly ILogger<GetRoleByIdRequest> _logger;
        private readonly IBus _bus;

        public GetRoleByIdRequest(
            IBus bus, 
            ILogger<GetRoleByIdRequest> logger)
        {
            _bus = bus;
            _logger = logger;
        }
        
        public async Task<IGetRoleByIdResultContract> Ask(
            IGetRoleByIdRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetRoleByIdRequestContract>(TimeSpan.FromSeconds(10));
                
            var response = await client
                .GetResponse<IGetRoleByIdSuccessResultContract, IGetRoleByIdErrorResultContract>(input);
                
            if (response.Is(out Response<IGetRoleByIdSuccessResultContract> successResponse))
                return successResponse.Message;
                
            if (response.Is(out Response<IGetRoleByIdErrorResultContract> errorResponse))
                return errorResponse.Message;
                
            throw new InvalidOperationException();
        }
    }
}
