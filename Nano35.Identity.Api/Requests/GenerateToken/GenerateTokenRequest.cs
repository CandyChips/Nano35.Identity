using MassTransit;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GenerateToken
{
    public class GenerateTokenRequest : 
        MasstransitRequest
        <IGenerateTokenRequestContract, 
            IGenerateTokenResultContract,
            IGenerateTokenSuccessResultContract, 
            IGenerateTokenErrorResultContract>
    {
        public GenerateTokenRequest(IBus bus, IGenerateTokenRequestContract request) : base(bus, request) {}
    }
}