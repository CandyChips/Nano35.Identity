using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GenerateToken
{
    public class GenerateTokenRequest :
        IPipelineNode<
            IGenerateTokenRequestContract,
            IGenerateTokenResultContract>
    {
        private readonly IBus _bus;

        public GenerateTokenRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IGenerateTokenResultContract> Ask(
            IGenerateTokenRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGenerateTokenRequestContract>(TimeSpan.FromSeconds(10));
                
            var response = await client
                .GetResponse<IGenerateTokenSuccessResultContract, IGenerateTokenErrorResultContract>(input);
                
            if (response.Is(out Response<IGenerateTokenSuccessResultContract> successResponse))
                return successResponse.Message;
                
            if (response.Is(out Response<IGenerateTokenErrorResultContract> errorResponse))
                return errorResponse.Message;
                
            throw new InvalidOperationException();
        }
    }
}
