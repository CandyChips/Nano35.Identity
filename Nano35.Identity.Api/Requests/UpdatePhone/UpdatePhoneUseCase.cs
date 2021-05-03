using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Identity.Api.Helpers;

namespace Nano35.Identity.Api.Requests.UpdatePhone
{
    public class UpdatePhoneUseCase : UseCaseEndPointNodeBase<IUpdatePhoneRequestContract, IUpdatePhoneResultContract>
    {
        private readonly IBus _bus;

        public UpdatePhoneUseCase(IBus bus) => _bus = bus;

        public override async Task<UseCaseResponse<IUpdatePhoneResultContract>> Ask(IUpdatePhoneRequestContract input) => 
            await new MasstransitUseCaseRequest<IUpdatePhoneRequestContract, IUpdatePhoneResultContract>(_bus, input)
                .GetResponse();
    }
}
