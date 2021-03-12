using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Api.Helpers;

namespace Nano35.Identity.Api.Requests.UpdateEmail
{
    public class UpdateEmailRequest :
        EndPointNodeBase<IUpdateEmailRequestContract, IUpdateEmailResultContract>
    {
        private readonly IBus _bus;

        public UpdateEmailRequest(IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IUpdateEmailResultContract> Ask(IUpdateEmailRequestContract input)
        {
            var client = _bus.CreateRequestClient<IUpdateEmailRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IUpdateEmailSuccessResultContract, IUpdateEmailErrorResultContract>(input);
            
            if (response.Is(out Response<IUpdateEmailSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IUpdateEmailErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}
