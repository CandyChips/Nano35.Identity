using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Identity.Api.Helpers;

namespace Nano35.Identity.Api.Requests.UpdatePhone
{
    public class UpdatePhone : EndPointNodeBase<IUpdatePhoneRequestContract, IUpdatePhoneResultContract>
    {
        private readonly IBus _bus;

        public UpdatePhone(IBus bus) => _bus = bus;

        public override async Task<UseCaseResponse<IUpdatePhoneResultContract>> Ask(IUpdatePhoneRequestContract input) => 
            await new MasstransitRequest<IUpdatePhoneRequestContract, IUpdatePhoneResultContract>(_bus, input).GetResponse();
    }
}
