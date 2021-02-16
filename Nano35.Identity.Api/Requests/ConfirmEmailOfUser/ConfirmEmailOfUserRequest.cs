using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.ConfirmEmailOfUser
{
    public class ConfirmEmailOfUserRequest :
        IPipelineNode<
            IGenerateTokenRequestContract,
            IGenerateTokenResultContract>
    {
        private readonly IBus _bus;

        public ConfirmEmailOfUserRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IGenerateTokenResultContract> Ask(
            IGenerateTokenRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGenerateTokenRequestContract>(TimeSpan.FromSeconds(10));
                
            var response = await client
                .GetResponse<
                    IGenerateTokenSuccessResultContract, 
                    IGenerateTokenErrorResultContract>(input);
                
            if (response.Is(out Response<IGenerateTokenSuccessResultContract> successResponse))
                return successResponse.Message;
                
            if (response.Is(out Response<IGenerateTokenErrorResultContract> errorResponse))
                return errorResponse.Message;
                
            throw new InvalidOperationException();
        }
    }
}
