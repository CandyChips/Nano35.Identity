using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GetAllUsers
{
    public class GetAllUsersUseCase :
        EndPointNodeBase<IGetAllUsersRequestContract, IGetAllUsersResultContract>
    {
        private readonly IBus _bus;

        public GetAllUsersUseCase(IBus bus) { _bus = bus; }
        
        public override async Task<IGetAllUsersResultContract> Ask(IGetAllUsersRequestContract input) => (await (new GetAllUsersRequest(_bus, input)).GetResponse(input));
    }
}
