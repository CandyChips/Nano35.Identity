using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.ConfirmEmailOfUser
{
    public class ConfirmEmailOfUserUseCase :
        EndPointNodeBase<IConfirmEmailOfUserRequestContract, IConfirmEmailOfUserResultContract>
    {
        private readonly IBus _bus;

        public ConfirmEmailOfUserUseCase(IBus bus) { _bus = bus; }
        
        public override async Task<IConfirmEmailOfUserResultContract> Ask(IConfirmEmailOfUserRequestContract input) => 
            (await (new ConfirmEmailOfUserRequest(_bus, input)).GetResponse(input));
    }
}
