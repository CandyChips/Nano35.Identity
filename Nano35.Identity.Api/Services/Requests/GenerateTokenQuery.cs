using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Services.Requests
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
            private readonly IBus _bus;
            public GenerateTokenHandler(IBus bus)
            {
                _bus = bus;
            }

            public async Task<IGenerateTokenResultContract> Handle(
                GenerateTokenQuery message,
                CancellationToken cancellationToken)
            {
                var client = _bus.CreateRequestClient<IGenerateTokenRequestContract>(TimeSpan.FromSeconds(10));
                var response = await client
                    .GetResponse<IGenerateTokenSuccessResultContract, IGenerateTokenErrorResultContract>(message, cancellationToken);
                if (response.Is(out Response<IGenerateTokenSuccessResultContract> successResponse))
                {
                    return successResponse.Message;
                }
                if (response.Is(out Response<IGenerateTokenErrorResultContract> errorResponse))
                {
                    return errorResponse.Message;
                }
                throw new InvalidOperationException();
            }
        }
    }
    
}