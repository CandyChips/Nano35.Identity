using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GetAllUsers
{
    public class GetAllUsersRequest :
        IPipelineNode<
            IGetAllUsersRequestContract, 
            IGetAllUsersResultContract>
    {
        private readonly IBus _bus;

        public GetAllUsersRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IGetAllUsersResultContract> Ask(
            IGetAllUsersRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetAllUsersRequestContract>(TimeSpan.FromSeconds(10));
            var response = await client
                .GetResponse<IGetAllUsersSuccessResultContract, IGetAllUsersErrorResultContract>(input);

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
