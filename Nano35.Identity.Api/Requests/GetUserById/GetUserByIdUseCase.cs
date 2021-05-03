using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Identity.Api.Requests.GetUserById
{
    public class GetUserByIdUseCase : UseCaseEndPointNodeBase<IGetUserByIdRequestContract, IGetUserByIdResultContract>
    {
        private readonly IBus _bus;
        public GetUserByIdUseCase(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<IGetUserByIdResultContract>> Ask(IGetUserByIdRequestContract input) => 
            await new MasstransitUseCaseRequest<IGetUserByIdRequestContract, IGetUserByIdResultContract>(_bus, input)
                .GetResponse();
    }
}
