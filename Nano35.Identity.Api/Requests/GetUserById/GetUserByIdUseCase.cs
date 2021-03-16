using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GetUserById
{
    public class GetUserByIdUseCase :
        EndPointNodeBase<IGetUserByIdRequestContract, IGetUserByIdResultContract>
    {
        private readonly IBus _bus;

        public GetUserByIdUseCase(IBus bus) { _bus = bus; }

        public override async Task<IGetUserByIdResultContract> Ask(IGetUserByIdRequestContract input) =>
            (await (new GetUserByIdRequest(_bus, input)).GetResponse(input));
    }
}
