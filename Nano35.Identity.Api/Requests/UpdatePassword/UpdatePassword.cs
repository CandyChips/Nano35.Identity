using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Identity.Api.Requests.UpdatePassword
{
    public class UpdatePassword : EndPointNodeBase<IUpdatePasswordRequestContract, IUpdatePasswordResultContract>
    {
        private readonly IBus _bus;
        public UpdatePassword(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<IUpdatePasswordResultContract>> Ask(IUpdatePasswordRequestContract input) => 
            await new MasstransitRequest<IUpdatePasswordRequestContract, IUpdatePasswordResultContract>(_bus, input).GetResponse();
    }
}
