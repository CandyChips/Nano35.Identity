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

namespace Nano35.Identity.Api.Services.Requests
{
    public class GetUsersByRoleIdResultViewModel :
        IGetUsersByRoleIdSuccessResultContract,
        IGetUsersByRoleIdNotFoundResultContract
    {
        public string Error { get; set; }
        public IEnumerable<IUserViewModel> Data { get; set; }
    }
    public class GetUsersByRoleIdQuery : 
        IGetUsersByRoleIdRequestContract, 
        IRequest<GetUsersByRoleIdResultViewModel>
    {
        public Guid Id { get; set; }
    }

    public class GetUsersByRoleIdHandler 
        : IRequestHandler<GetUsersByRoleIdQuery, GetUsersByRoleIdResultViewModel>
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

        public async Task<GetUsersByRoleIdResultViewModel> Handle(
            GetUsersByRoleIdQuery message,
            CancellationToken cancellationToken)
        {
            var result = new GetUsersByRoleIdResultViewModel();
            var client = _bus.CreateRequestClient<IGetUsersByRoleIdRequestContract>(TimeSpan.FromSeconds(10));
            var response = await client
                .GetResponse<IGetUsersByRoleIdSuccessResultContract, IGetUsersByRoleIdNotFoundResultContract>(message, cancellationToken);

            if (response.Is(out Response<IGetUsersByRoleIdSuccessResultContract> successResponse))
            {
                result.Data = successResponse.Message.Data;
                return result;
            }
            
            if (response.Is(out Response<IGetUsersByRoleIdNotFoundResultContract> errorResponse))
            {
                result.Error = "Не найдено";
                return result;
            }
            
            throw new InvalidOperationException();
        }
    }
}