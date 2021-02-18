using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.CreateUser
{
    public class CreateUserRequest :
        IPipelineNode<ICreateUserRequestContract, ICreateUserResultContract>
    {
        private readonly IBus _bus;

        public CreateUserRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<ICreateUserResultContract> Ask(
            ICreateUserRequestContract input)
        {
            var client = _bus.CreateRequestClient<ICreateUserRequestContract>(TimeSpan.FromSeconds(10));
                
            var response = await client
                .GetResponse<ICreateUserSuccessResultContract, ICreateUserErrorResultContract>(input);
                
            if (response.Is(out Response<ICreateUserSuccessResultContract> successResponse))
                return successResponse.Message;
                
            if (response.Is(out Response<ICreateUserErrorResultContract> errorResponse))
                return errorResponse.Message;
                
            throw new InvalidOperationException();
        }
    }
}
