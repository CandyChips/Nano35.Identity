using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Contracts.Users.Artifacts;
using IGetUserByIdNotFoundResultContract = Nano35.Contracts.Identity.Artifacts.IGetUserByIdNotFoundResultContract;
using IGetUserByIdRequestContract = Nano35.Contracts.Identity.Artifacts.IGetUserByIdRequestContract;
using IGetUserByIdSuccessResultContract = Nano35.Contracts.Identity.Artifacts.IGetUserByIdSuccessResultContract;

namespace Nano35.Identity.Api.Services.Requests
{
    public class GetUserByIdResultViewModel :
        IGetUserByIdSuccessResultContract,
        IGetUserByIdNotFoundResultContract
    {
        public string Error { get; set; }
        public IUserViewModel Data { get; set; }
    }
    public class GetUserByIdQuery 
        : IGetUserByIdRequestContract, IRequest<GetUserByIdResultViewModel>
    {
        public Guid UserId { get; set; }
    }

    public class GetUserByIdHandler 
        : IRequestHandler<GetUserByIdQuery, GetUserByIdResultViewModel>
    {
        private readonly ILogger<GetUserByIdHandler> _logger;
        private readonly IBus _bus;
        public GetUserByIdHandler(
            IBus bus, 
            ILogger<GetUserByIdHandler> logger)
        {
            _bus = bus;
            _logger = logger;
        }

        public async Task<GetUserByIdResultViewModel> Handle(
            GetUserByIdQuery message,
            CancellationToken cancellationToken)
        {
            var result = new GetUserByIdResultViewModel();
            var client = _bus.CreateRequestClient<IGetUserByIdRequestContract>(TimeSpan.FromSeconds(10));
            var response = await client
                .GetResponse<IGetUserByIdSuccessResultContract, IGetUserByIdNotFoundResultContract>(message, cancellationToken);

            if (response.Is(out Response<IGetUserByIdSuccessResultContract> successResponse))
            {
                result.Data = successResponse.Message.Data;
                return result;
            }
            
            if (response.Is(out Response<IGetUserByIdNotFoundResultContract> errorResponse))
            {
                result.Error = "Не найдено";
                return result;
            }
            
            throw new InvalidOperationException();
        }
    }
}