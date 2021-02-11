using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GetUserById
{
    public class GetUserByIdRequest :
        IPipelineNode<IGetUserByIdRequestContract, IGetUserByIdResultContract>
    {
        public Guid UserId { get; set; }
        
        private readonly IBus _bus;
        private readonly ILogger<GetUserByIdRequest> _logger;

        public GetUserByIdRequest(
            IBus bus, 
            ILogger<GetUserByIdRequest> logger)
        {
            _bus = bus;
            _logger = logger;
        }
        
        public async Task<IGetUserByIdResultContract> Ask(
            IGetUserByIdRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetUserByIdRequestContract>(TimeSpan.FromSeconds(10));
                
            var response = await client
                .GetResponse<IGetUserByIdSuccessResultContract, IGetUserByIdErrorResultContract>(input);

            if (response.Is(out Response<IGetUserByIdSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetUserByIdErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}
