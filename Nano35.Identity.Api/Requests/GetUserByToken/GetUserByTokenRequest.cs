using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Api.Helpers;

namespace Nano35.Identity.Api.Requests.GetUserByToken
{
    public class GetUserByTokenRequest :
        IPipelineNode<IGetUserByIdRequestContract, IGetUserByIdResultContract>
    {
        
        private readonly IBus _bus;
        private readonly ICustomAuthStateProvider _auth;

        public GetUserByTokenRequest(
            IBus bus, ICustomAuthStateProvider auth)
        {
            _bus = bus;
            _auth = auth;
        }
        
        public async Task<IGetUserByIdResultContract> Ask(
            IGetUserByIdRequestContract input)
        {
            input.UserId = _auth.CurrentUserId;
            
            var client = _bus.CreateRequestClient<IGetUserByIdRequestContract>(TimeSpan.FromSeconds(10));
                
            var response = await client
                .GetResponse<IGetUserByIdSuccessResultContract, IGetUserByIdErrorResultContract>(input);

            if (response.Is(out Response<IGetUserByIdSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetUserByIdErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}
