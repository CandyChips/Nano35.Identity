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
        EndPointRequestNodeBase<IGenerateTokenRequestContract, IGenerateTokenResultContract, IGenerateTokenSuccessResultContract, IGenerateTokenErrorResultContract>
    {
        public GenerateTokenUseCase(IBus bus) : base(bus) {}
    }
}
