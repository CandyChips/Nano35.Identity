using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Identity.Api.Requests.GetAllUsers
{
    public class GetAllUsersUseCase : UseCaseEndPointNodeBase<IGetAllUsersRequestContract, IGetAllUsersResultContract>
    {
        private readonly IBus _bus;
        public GetAllUsersUseCase(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<IGetAllUsersResultContract>> Ask(IGetAllUsersRequestContract input) => 
            await new MasstransitUseCaseRequest<IGetAllUsersRequestContract, IGetAllUsersResultContract>(_bus, input)
                .GetResponse();
    }
}
