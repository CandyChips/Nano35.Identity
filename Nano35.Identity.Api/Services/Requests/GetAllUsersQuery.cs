using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Identity.Api.Services.Helpers;

namespace Nano35.Identity.Api.Services.Requests
{
    public class GetAllUsersQuery :
        IGetAllUsersRequestContract,
        IRequest<IGetAllUsersResultContract>
    {
        public class GetAllUsersHandler : 
            IRequestHandler<GetAllUsersQuery, IGetAllUsersResultContract>
        {
            private readonly ILogger<GetAllUsersHandler> _logger;
            private readonly IBus _bus;
        
            public GetAllUsersHandler(
                IBus bus, 
                ILogger<GetAllUsersHandler> logger)
            {
                _bus = bus;
                _logger = logger;
            }
        
            public async Task<IGetAllUsersResultContract> Handle(
                GetAllUsersQuery request,
                CancellationToken cancellationToken)
            {
                var client = _bus.CreateRequestClient<IGetAllUsersRequestContract>(TimeSpan.FromSeconds(10));
                var response = await client
                    .GetResponse<IGetAllUsersSuccessResultContract, IGetAllUsersErrorResultContract>(request, cancellationToken);

                if (response.Is(out Response<IGetAllUsersSuccessResultContract> successResponse))
                {
                    return successResponse.Message;
                }
                if (response.Is(out Response<IGetAllUsersErrorResultContract> errorResponse))
                {
                    return errorResponse.Message;
                }
                throw new InvalidOperationException();
            }
        }
    }

}