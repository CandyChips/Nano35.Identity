using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Identity.Api.Requests.UpdatePassword
{
    public class UpdatePasswordUseCase : UseCaseEndPointNodeBase<IUpdatePasswordRequestContract, IUpdatePasswordResultContract>
    {
        private readonly IBus _bus;
        public UpdatePasswordUseCase(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<IUpdatePasswordResultContract>> Ask(IUpdatePasswordRequestContract input) => 
            await new MasstransitUseCaseRequest<IUpdatePasswordRequestContract, IUpdatePasswordResultContract>(_bus, input)
                .GetResponse();
    }
}
