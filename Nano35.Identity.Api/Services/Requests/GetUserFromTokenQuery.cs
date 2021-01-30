using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Identity.Api.Services.Helpers;

namespace Nano35.Identity.Api.Services.Requests
{
    public class GetUserFromTokenQuery : 
        IGetUserByIdRequestContract, 
        IRequest<IGetUserByIdResultContract>
    {
        public Guid UserId { get; set; }
        
        public class GetUserFromTokenHandler 
            : IRequestHandler<GetUserFromTokenQuery, IGetUserByIdResultContract>
        {
            private readonly ILogger<GetUserFromTokenHandler> _logger;
            private readonly ICustomAuthStateProvider _auth;
            private readonly IBus _bus;
            public GetUserFromTokenHandler(
                ICustomAuthStateProvider auth, 
                IBus bus,
                ILogger<GetUserFromTokenHandler> logger)
            {
                _auth = auth;
                _bus = bus;
                _logger = logger;
            }

            public async Task<IGetUserByIdResultContract> Handle(
                GetUserFromTokenQuery message,
                CancellationToken cancellationToken)
            {
                message.UserId = _auth.CurrentUserId;
                
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