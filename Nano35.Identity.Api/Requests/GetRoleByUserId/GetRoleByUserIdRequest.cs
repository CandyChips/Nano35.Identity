using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GetRoleByUserId
{
    public class GetRoleByUserIdRequest :
        IPipelineNode<IGetRoleByUserIdRequestContract, IGetRoleByUserIdResultContract>
    {
        public Guid UserId { get; set; }
        
        private readonly ILogger<GetRoleByUserIdRequest> _logger;
        private readonly IBus _bus;

        public GetRoleByUserIdRequest(
            IBus bus, 
            ILogger<GetRoleByUserIdRequest> logger)
        {
            _bus = bus;
            _logger = logger;
        }
        
        public async Task<IGetRoleByUserIdResultContract> Ask(
            IGetRoleByUserIdRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetRoleByUserIdRequestContract>(TimeSpan.FromSeconds(10));
                
            var response = await client
                .GetResponse<IGetRoleByUserIdSuccessResultContract, IGetRoleByUserIdErrorResultContract>(input);
                
            if (response.Is(out Response<IGetRoleByUserIdSuccessResultContract> successResponse))
                return successResponse.Message;
                
            if (response.Is(out Response<IGetRoleByUserIdErrorResultContract> errorResponse))
                return errorResponse.Message;
                
            throw new InvalidOperationException();
        }
    }
}
