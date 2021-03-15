using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.CreateUser
{
    public class CreateUserUseCase :
        EndPointNodeBase<ICreateUserRequestContract, ICreateUserResultContract>
    {
        private readonly IBus _bus;

        public CreateUserUseCase(IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<ICreateUserResultContract> Ask(ICreateUserRequestContract input) => (await (new CreateUserRequest(_bus, input)).GetResponse());
    }
}
