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
    public class GetRoleByUserIdQuery : 
        IGetRoleByUserIdRequestContract, 
        IRequest<IGetRoleByUserIdResultContract>
    {
        public Guid UserId { get; set; }
        
        public class GetRoleByUserIdHandler 
            : IRequestHandler<GetRoleByUserIdQuery, IGetRoleByUserIdResultContract>
        {
            private readonly IBus _bus;
            public GetRoleByUserIdHandler(IBus bus)
            {
                _bus = bus;
            }

            public async Task<IGetRoleByUserIdResultContract> Handle(
                GetRoleByUserIdQuery message,
                CancellationToken cancellationToken)
            {
                var client = _bus.CreateRequestClient<IGetRoleByUserIdRequestContract>(TimeSpan.FromSeconds(10));
                var response = await client
                    .GetResponse<IGetRoleByUserIdSuccessResultContract, IGetRoleByUserIdErrorResultContract>(message, cancellationToken);
                if (response.Is(out Response<IGetRoleByUserIdSuccessResultContract> successResponse))
                {
                    return successResponse.Message;
                }
                if (response.Is(out Response<IGetRoleByUserIdErrorResultContract> errorResponse))
                {
                    return errorResponse.Message;
                }
                throw new InvalidOperationException();
            }
        }
    }
}