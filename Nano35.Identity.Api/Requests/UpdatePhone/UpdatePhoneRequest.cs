using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Api.Helpers;

namespace Nano35.Identity.Api.Requests.UpdatePhone
{
    public class UpdatePhoneRequest :
        IPipelineNode<IUpdatePhoneRequestContract, IUpdatePhoneResultContract>
    {
        public Guid UserId { get; set; }
        public string Phone { get; set; }
        
        private readonly IBus _bus;
        private readonly ILogger<UpdatePhoneRequest> _logger;
        private readonly ICustomAuthStateProvider _customAuthStateProvider;

        public UpdatePhoneRequest(
            IBus bus, 
            ICustomAuthStateProvider customAuthStateProvider, 
            ILogger<UpdatePhoneRequest> logger)
        {
            _bus = bus;
            _customAuthStateProvider = customAuthStateProvider;
            _logger = logger;
        }
        
        public async Task<IUpdatePhoneResultContract> Ask(
            IUpdatePhoneRequestContract input)
        {
            var client = _bus.CreateRequestClient<IUpdatePhoneRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IUpdatePhoneSuccessResultContract, IUpdatePhoneErrorResultContract>(input);
            
            if (response.Is(out Response<IUpdatePhoneSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IUpdatePhoneErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}
