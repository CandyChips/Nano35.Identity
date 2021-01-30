using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using IGetUserByIdRequestContract = Nano35.Contracts.Identity.Artifacts.IGetUserByIdRequestContract;
using IGetUserByIdSuccessResultContract = Nano35.Contracts.Identity.Artifacts.IGetUserByIdSuccessResultContract;

namespace Nano35.Identity.Api.Services.Requests
{
    public class GetUserByIdQuery : 
        IGetUserByIdRequestContract, 
        IRequest<IGetUserByIdResultContract>
    {
        public Guid UserId { get; set; }
        
        public class GetUserByIdHandler 
            : IRequestHandler<GetUserByIdQuery, IGetUserByIdResultContract>
        {
            private readonly IBus _bus;
            private readonly ILogger<GetUserByIdHandler> _logger;
            public GetUserByIdHandler(
                IBus bus,
                ILogger<GetUserByIdHandler> logger)
            {
                _bus = bus;
                _logger = logger;
            }

            public async Task<IGetUserByIdResultContract> Handle(
                GetUserByIdQuery message,
                CancellationToken cancellationToken)
            {
                var client = _bus.CreateRequestClient<IGetUserByIdRequestContract>(TimeSpan.FromSeconds(10));
                
                var response = await client
                    .GetResponse<IGetUserByIdSuccessResultContract, IGetUserByIdErrorResultContract>(message, cancellationToken);

                if (response.Is(out Response<IGetUserByIdSuccessResultContract> successResponse))
                    return successResponse.Message;
            
                if (response.Is(out Response<IGetUserByIdErrorResultContract> errorResponse))
                    return errorResponse.Message;
            
                throw new InvalidOperationException();
            }
        }
    }

}