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
    public class GetRoleByUserIdResultViewModel :
        IGetRoleByUserIdSuccessResultContract,
        IGetRoleByUserIdNotFoundResultContract
    {
        public string Error { get; set; }
        public IRoleViewModel Data { get; set; }
    }
    public class GetRoleByUserIdQuery : 
        IGetRoleByUserIdRequestContract, 
        IRequest<GetRoleByUserIdResultViewModel>
    {
        public Guid UserId { get; set; }
    }

    public class GetRoleByUserIdHandler 
        : IRequestHandler<GetRoleByUserIdQuery, GetRoleByUserIdResultViewModel>
    {
        private readonly ILogger<GetRoleByUserIdHandler> _logger;
        private readonly IBus _bus;
        public GetRoleByUserIdHandler(
            IBus bus, 
            ILogger<GetRoleByUserIdHandler> logger)
        {
            _bus = bus;
            _logger = logger;
        }

        public async Task<GetRoleByUserIdResultViewModel> Handle(
            GetRoleByUserIdQuery message,
            CancellationToken cancellationToken)
        {
            var result = new GetRoleByUserIdResultViewModel();
            var client = _bus.CreateRequestClient<IGetRoleByUserIdRequestContract>(TimeSpan.FromSeconds(10));
            var response = await client
                .GetResponse<IGetRoleByUserIdSuccessResultContract, IGetRoleByUserIdNotFoundResultContract>(message, cancellationToken);

            if (response.Is(out Response<IGetRoleByUserIdSuccessResultContract> successResponse))
            {
                result.Data = successResponse.Message.Data;
                return result;
            }
            
            if (response.Is(out Response<IGetRoleByUserIdNotFoundResultContract> errorResponse))
            {
                result.Error = "Не найдено";
                return result;
            }
            
            throw new InvalidOperationException();
        }
    }
}