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
        IPipelineNode<
            IGetUserByIdRequestContract,
            IGetUserByIdResultContract>
    {
        private readonly IBus _bus;

        public GetUserByIdRequest(
            IBus bus)
        {
            _bus = bus;
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
