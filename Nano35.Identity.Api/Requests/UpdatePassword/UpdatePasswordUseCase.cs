using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Api.Helpers;

namespace Nano35.Identity.Api.Requests.UpdatePassword
{
    public class UpdatePasswordUseCase :
        EndPointNodeBase<IUpdatePasswordRequestContract, IUpdatePasswordResultContract>
    {
        private readonly IBus _bus;

        public UpdatePasswordUseCase(IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IUpdatePasswordResultContract> Ask(IUpdatePasswordRequestContract input)
        {
            var request = new UpdatePasswordRequest(_bus, input);
            return (await request.GetResponse());
        }
    }
}
