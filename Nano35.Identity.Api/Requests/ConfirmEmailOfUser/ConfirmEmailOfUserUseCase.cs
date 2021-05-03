using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Identity.Api.Requests.ConfirmEmailOfUser
{
    public class ConfirmEmailOfUserUseCase :
        UseCaseEndPointNodeBase<IConfirmEmailOfUserRequestContract, IConfirmEmailOfUserResultContract>
    {
        private readonly IBus _bus;
        public ConfirmEmailOfUserUseCase(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<IConfirmEmailOfUserResultContract>> Ask(IConfirmEmailOfUserRequestContract input) => 
            await new MasstransitUseCaseRequest<IConfirmEmailOfUserRequestContract, IConfirmEmailOfUserResultContract>(_bus, input)
                .GetResponse();
    }
}
