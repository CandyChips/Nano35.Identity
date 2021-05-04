using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Identity.Api.Requests.UpdateName
{
    public class UpdateName : EndPointNodeBase<IUpdateNameRequestContract, IUpdateNameResultContract>
    {
        private readonly IBus _bus;
        public UpdateName(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<IUpdateNameResultContract>> Ask(IUpdateNameRequestContract input) => 
            await new MasstransitRequest<IUpdateNameRequestContract, IUpdateNameResultContract>(_bus, input).GetResponse();
    }
}
