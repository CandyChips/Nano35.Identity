using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.Register
{
    public class RegisterRequest :
        IPipelineNode<IRegisterRequestContract, IRegisterResultContract>
    {
        public Guid NewUserId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        
        private readonly ILogger<RegisterRequest> _logger;
        private readonly IBus _bus;

        public RegisterRequest(
            IBus bus,
            ILogger<RegisterRequest> logger)
        {
            _bus = bus;
            _logger = logger;
        }

        public async Task<IRegisterResultContract> Ask(
            IRegisterRequestContract input)
        {
            var client = _bus.CreateRequestClient<IRegisterRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IRegisterSuccessResultContract, IRegisterErrorResultContract>(input);
            
            if (response.Is(out Response<IRegisterSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IRegisterErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}
