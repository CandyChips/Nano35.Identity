using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests
{
    public class GetUsersByRoleIdQuery : 
        IGetUsersByRoleIdRequestContract, 
        IRequest<IGetUsersByRoleIdResultContract>
    {
        public Guid Id { get; set; }
    }

    public class GetUsersByRoleIdHandler 
        : IRequestHandler<GetUsersByRoleIdQuery, IGetUsersByRoleIdResultContract>
    {
        private readonly ILogger<GetUsersByRoleIdHandler> _logger;
        private readonly IBus _bus;
        public GetUsersByRoleIdHandler(
            IBus bus, 
            ILogger<GetUsersByRoleIdHandler> logger)
        {
            _bus = bus;
            _logger = logger;
        }

        public async Task<IGetUsersByRoleIdResultContract> Handle(
            GetUsersByRoleIdQuery message,
            CancellationToken cancellationToken)
        {
            var client = _bus.CreateRequestClient<IGetUsersByRoleIdRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetUsersByRoleIdSuccessResultContract, IGetUsersByRoleIdNotFoundResultContract>(message, cancellationToken);
            
            if (response.Is(out Response<IGetUsersByRoleIdSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetUsersByRoleIdNotFoundResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}