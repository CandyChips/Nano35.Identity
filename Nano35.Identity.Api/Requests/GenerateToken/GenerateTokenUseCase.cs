using MassTransit;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GenerateToken
{
    public class GenerateTokenUseCase :
        EndPointRequestNodeBase<IGenerateTokenRequestContract, IGenerateTokenResultContract, IGenerateTokenSuccessResultContract, IGenerateTokenErrorResultContract>
    {
        public GenerateTokenUseCase(IBus bus) : base(bus) {}
    }
}
