using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;

namespace Nano35.Identity.Api.Services.Requests
{
    public class GetRoleByIdQuery : 
        IGetRoleByIdRequestContract, 
        IRequest<IGetRoleByIdResultContract>
    {
        public Guid RoleId { get; set; }
        
        public class GetRoleByIdHandler : 
            IRequestHandler<GetRoleByIdQuery, IGetRoleByIdResultContract>
        {
            private readonly ILogger<GetRoleByIdHandler> _logger;
            private readonly IBus _bus;
            public GetRoleByIdHandler(
                IBus bus, 
                ILogger<GetRoleByIdHandler> logger)
            {
                _bus = bus;
                _logger = logger;
            }

            public async Task<IGetRoleByIdResultContract> Handle(
                GetRoleByIdQuery message,
                CancellationToken cancellationToken)
            {
                var client = _bus.CreateRequestClient<IGetRoleByIdRequestContract>(TimeSpan.FromSeconds(10));
                var response = await client
                    .GetResponse<IGetRoleByIdSuccessResultContract, IGetRoleByIdErrorResultContract>(message, cancellationToken);
                if (response.Is(out Response<IGetRoleByIdSuccessResultContract> successResponse))
                {
                    return successResponse.Message;
                }
                if (response.Is(out Response<IGetRoleByIdErrorResultContract> errorResponse))
                {
                    return errorResponse.Message;
                }
                throw new InvalidOperationException();
            }
        }
    }
}