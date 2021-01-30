using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests
{
    public class GenerateTokenQuery : 
        IGenerateTokenRequestContract, 
        IRequest<IGenerateTokenResultContract>
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public class GenerateTokenHandler : 
            IRequestHandler<GenerateTokenQuery, IGenerateTokenResultContract>
        {
            private readonly ILogger<GenerateTokenHandler> _logger;
            private readonly IBus _bus;
            public GenerateTokenHandler(
                IBus bus, 
                ILogger<GenerateTokenHandler> logger)
            {
                _bus = bus;
                _logger = logger;
            }

            public async Task<IGenerateTokenResultContract> Handle(
                GenerateTokenQuery message,
                CancellationToken cancellationToken)
            {
                var client = _bus.CreateRequestClient<IGenerateTokenRequestContract>(TimeSpan.FromSeconds(10));
                
                var response = await client
                    .GetResponse<IGenerateTokenSuccessResultContract, IGenerateTokenErrorResultContract>(message, cancellationToken);
                
                if (response.Is(out Response<IGenerateTokenSuccessResultContract> successResponse))
                    return successResponse.Message;
                
                if (response.Is(out Response<IGenerateTokenErrorResultContract> errorResponse))
                    return errorResponse.Message;
                
                throw new InvalidOperationException();
            }
        }
    }
    
}