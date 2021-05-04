using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Identity.Api.Requests.GetUserById
{
    public class GetUserById : EndPointNodeBase<IGetUserByIdRequestContract, IGetUserByIdResultContract>
    {
        private readonly IBus _bus;
        public GetUserById(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<IGetUserByIdResultContract>> Ask(IGetUserByIdRequestContract input) => 
            await new MasstransitRequest<IGetUserByIdRequestContract, IGetUserByIdResultContract>(_bus, input).GetResponse();
    }
}
