using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Identity.Api.Requests.ConfirmEmailOfUser
{
    public class ConfirmEmailOfUser :
        EndPointNodeBase<IConfirmEmailOfUserRequestContract, IConfirmEmailOfUserResultContract>
    {
        private readonly IBus _bus;
        public ConfirmEmailOfUser(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<IConfirmEmailOfUserResultContract>> Ask(IConfirmEmailOfUserRequestContract input) => 
            await new MasstransitRequest<IConfirmEmailOfUserRequestContract, IConfirmEmailOfUserResultContract>(_bus, input).GetResponse();
    }
}
