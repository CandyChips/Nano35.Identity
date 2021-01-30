using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests
{

    public class GetAllRolesQuery :
        IGetAllRolesRequestContract,
        IRequest<IGetAllRolesResultContract>
    {
        public class GetAllRolesHandler 
            : IRequestHandler<GetAllRolesQuery, IGetAllRolesResultContract>
        {
            private readonly ILogger<GetAllRolesHandler> _logger;
            private readonly IBus _bus;
            public GetAllRolesHandler(
                IBus bus, 
                ILogger<GetAllRolesHandler> logger)
            {
                _bus = bus;
                _logger = logger;
            }

            public async Task<IGetAllRolesResultContract> Handle(
                GetAllRolesQuery message,
                CancellationToken cancellationToken)
            {
                var client = _bus.CreateRequestClient<IGetAllRolesRequestContract>(TimeSpan.FromSeconds(10));
                
                var response = await client
                    .GetResponse<IGetAllRolesResultContract, IGetAllRolesErrorResultContract>(message, cancellationToken);

                if (response.Is(out Response<IGetAllRolesResultContract> successResponse))
                    return successResponse.Message;
            
                if (response.Is(out Response<IGetAllRolesErrorResultContract> errorResponse))
                    return errorResponse.Message;
            
                throw new InvalidOperationException();
            }
        }
        
    }
}