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
    public class GetRoleByIdResultViewModel :
        IGetRoleByIdSuccessResultContract,
        IGetRoleByIdNotFoundResultContract
    {
        public string Error { get; set; }
        public IRoleViewModel Data { get; set; }
    }
    public class GetRoleByIdQuery : 
        IGetRoleByIdRequestContract, 
        IRequest<GetRoleByIdResultViewModel>
    {
        public Guid RoleId { get; set; }
    }

    public class GetRoleByIdHandler : 
        IRequestHandler<GetRoleByIdQuery, GetRoleByIdResultViewModel>
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

        public async Task<GetRoleByIdResultViewModel> Handle(
            GetRoleByIdQuery message,
            CancellationToken cancellationToken)
        {
            var result = new GetRoleByIdResultViewModel();
            var client = _bus.CreateRequestClient<IGetRoleByIdRequestContract>(TimeSpan.FromSeconds(10));
            var response = await client
                .GetResponse<IGetRoleByIdSuccessResultContract, IGetRoleByIdNotFoundResultContract>(message, cancellationToken);

            if (response.Is(out Response<IGetRoleByIdSuccessResultContract> successResponse))
            {
                result.Data = successResponse.Message.Data;
                return result;
            }
            
            if (response.Is(out Response<IGetRoleByIdNotFoundResultContract> errorResponse))
            {
                result.Error = "Не найдено";
                return result;
            }
            
            throw new InvalidOperationException();
        }
    }
}