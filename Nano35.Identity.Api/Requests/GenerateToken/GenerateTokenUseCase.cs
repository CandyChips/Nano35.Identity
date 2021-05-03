using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Identity.Api.Requests.GenerateToken
{
    public class GenerateTokenUseCase : UseCaseEndPointNodeBase<IGenerateTokenRequestContract, IGenerateTokenResultContract>
    {
        private readonly IBus _bus;
        public GenerateTokenUseCase(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<IGenerateTokenResultContract>> Ask(IGenerateTokenRequestContract input) => 
            await new MasstransitUseCaseRequest<IGenerateTokenRequestContract, IGenerateTokenResultContract>(_bus, input)
                .GetResponse();
    }
}
