using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Api.Helpers;

namespace Nano35.Identity.Api.Requests.GetUserByToken
{
    public class GetUserByTokenUseCase :
        EndPointNodeBase<IGetUserByIdRequestContract, IGetUserByIdResultContract>
    {
        private readonly IBus _bus;
        private readonly ICustomAuthStateProvider _auth;

        public GetUserByTokenUseCase(IBus bus, ICustomAuthStateProvider auth)
        {
            _bus = bus;
            _auth = auth;
        }
        
        public override async Task<IGetUserByIdResultContract> Ask(IGetUserByIdRequestContract input)
        {
            input.UserId = _auth.CurrentUserId;
            var request = new GetUserByIdRequest(_bus, input);
            return (await request.GetResponse(input));
        }
    }
}
