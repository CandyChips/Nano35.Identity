using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Api.Helpers;

namespace Nano35.Identity.Api.Requests.UpdateName
{
    public class UpdateNameUseCase :
        EndPointNodeBase<IUpdateNameRequestContract, IUpdateNameResultContract>
    {
        private readonly IBus _bus;

        public UpdateNameUseCase(IBus bus) { _bus = bus; }

        public override async Task<IUpdateNameResultContract> Ask(IUpdateNameRequestContract input) => 
            (await (new UpdateNameRequest(_bus, input)).GetResponse(input));
    }
}
