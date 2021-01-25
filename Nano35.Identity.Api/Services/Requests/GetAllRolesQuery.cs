using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Identity.Api.Services.Helpers;

namespace Nano35.Identity.Api.Services.Requests
{
    public class GetAllRolesResultViewModel :
        IGetAllRolesResultContract,
        IGetAllRolesNotFoundResultContract
    {
        public IEnumerable<IRoleViewModel> Data { get; set; }
        public string Error { get; set; }
    }
    
    public class GetAllRolesQuery : 
        IGetAllRolesRequestContract, 
        IRequest<GetAllRolesResultViewModel> {  }

    public class GetAllRolesHandler 
        : IRequestHandler<GetAllRolesQuery, GetAllRolesResultViewModel>
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

        public async Task<GetAllRolesResultViewModel> Handle(
            GetAllRolesQuery message,
            CancellationToken cancellationToken)
        {
            var result = new GetAllRolesResultViewModel();
            var client = _bus.CreateRequestClient<IGetAllRolesRequestContract>(TimeSpan.FromSeconds(10));
            var response = await client
                .GetResponse<IGetAllRolesResultContract, IGetAllRolesNotFoundResultContract>(message, cancellationToken);

            if (response.Is(out Response<IGetAllRolesResultContract> successResponse))
            {
                result.Data = successResponse.Message.Data;
                return result;
            }
            
            if (response.Is(out Response<IGetAllRolesNotFoundResultContract> errorResponse))
            {
                result.Error = "Не найдено";
                return result;
            }
            
            throw new InvalidOperationException();
        }
    }
}