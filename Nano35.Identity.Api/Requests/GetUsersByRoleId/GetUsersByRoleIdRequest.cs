using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Api.Helpers;

namespace Nano35.Identity.Api.Requests.GetUsersByRoleId
{
    public class GetUsersByRoleIdRequest :
        IPipelineNode<IGetUsersByRoleIdRequestContract, IGetUsersByRoleIdResultContract>
    {
        public Guid Id { get; set; }
        
        private readonly ILogger<GetUsersByRoleIdQuery> _logger;
        private readonly IBus _bus;

        public GetUsersByRoleIdRequest(
            IBus bus,
            ILogger<GetUsersByRoleIdQuery> logger)
        {
            _bus = bus;
            _logger = logger;
        }
        
        public async Task<IGetUsersByRoleIdResultContract> Ask(
            IGetUsersByRoleIdRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetUsersByRoleIdRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetUsersByRoleIdSuccessResultContract, IGetUsersByRoleIdNotFoundResultContract>(input);
            
            if (response.Is(out Response<IGetUsersByRoleIdSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetUsersByRoleIdNotFoundResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}
