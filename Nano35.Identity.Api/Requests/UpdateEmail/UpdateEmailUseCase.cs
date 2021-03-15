using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Api.Helpers;

namespace Nano35.Identity.Api.Requests.UpdateEmail
{
    public class UpdateEmailUseCase :
        EndPointNodeBase<IUpdateEmailRequestContract, IUpdateEmailResultContract>
    {
        private readonly IBus _bus;

        public UpdateEmailUseCase(IBus bus) { _bus = bus; }
        
        public override async Task<IUpdateEmailResultContract> Ask(IUpdateEmailRequestContract input) => (await (new UpdateEmailRequest(_bus, input)).GetResponse());
    }
}
