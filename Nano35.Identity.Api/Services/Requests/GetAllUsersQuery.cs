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
    public class GetAllUsersResultViewModel :
        IGetAllUsersSuccessResultContract,
        IGetAllUsersNotFoundResultContract
    {
        public IEnumerable<IUserViewModel> Data { get; set; }
        public string Error { get; set; }
    }
    
    public class GetAllUsersQuery :
        IGetAllUsersRequestContract, 
        IRequest<GetAllUsersResultViewModel> {  }

    public class GetAllUsersHandler : 
        IRequestHandler<GetAllUsersQuery, GetAllUsersResultViewModel>
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
        
        public async Task<GetAllUsersResultViewModel> Handle(
            GetAllUsersQuery request,
            CancellationToken cancellationToken)
        {
            var result = new GetAllUsersResultViewModel();
            var client = _bus.CreateRequestClient<IGetAllUsersRequestContract>(TimeSpan.FromSeconds(10));
            var response = await client
                .GetResponse<IGetAllUsersSuccessResultContract, IGetAllUsersNotFoundResultContract>(request);

            if (response.Is(out Response<IGetAllUsersSuccessResultContract> successResponse))
            {
                result.Data = successResponse.Message.Data;
                return result;
            }
            if (response.Is(out Response<IGetAllUsersNotFoundResultContract> errorResponse))
            {
                result.Error = "Не найдено";
                return result;
            }
            throw new InvalidOperationException();
        }
    }
}