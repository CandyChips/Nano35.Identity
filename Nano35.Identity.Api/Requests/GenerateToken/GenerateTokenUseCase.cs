using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GenerateToken
{
    public class GenerateTokenUseCase :
        EndPointNodeBase<IGenerateTokenRequestContract, IGenerateTokenResultContract>
    {
        private readonly IBus _bus;

        public GenerateTokenUseCase(IBus bus) { _bus = bus; }

        public override async Task<IGenerateTokenResultContract> Ask(IGenerateTokenRequestContract input) => (await (new GenerateTokenRequest(_bus, input)).GetResponse());
    }
}
