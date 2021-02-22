using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Api.Helpers;

namespace Nano35.Identity.Api.Requests.UpdateName
{
    public class UpdateNameRequest :
        IPipelineNode<
            IUpdateNameRequestContract, 
            IUpdateNameResultContract>
    {
        private readonly IBus _bus;

        public UpdateNameRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IUpdateNameResultContract> Ask(
            IUpdateNameRequestContract input)
        {
            var client = _bus.CreateRequestClient<IUpdateNameRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IUpdateNameSuccessResultContract, IUpdateNameErrorResultContract>(input);
            
            if (response.Is(out Response<IUpdateNameSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IUpdateNameErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}
