using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Api.Helpers;

namespace Nano35.Identity.Api.Requests.UpdatePassword
{
    public class UpdatePasswordRequest :
        IPipelineNode<IUpdatePasswordRequestContract, IUpdatePasswordResultContract>
    {
        private readonly IBus _bus;

        public UpdatePasswordRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IUpdatePasswordResultContract> Ask(
            IUpdatePasswordRequestContract input)
        {
            var client = _bus.CreateRequestClient<IUpdatePasswordRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IUpdatePasswordSuccessResultContract, IUpdatePasswordErrorResultContract>(input);
            
            if (response.Is(out Response<IUpdatePasswordSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IUpdatePasswordErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}
