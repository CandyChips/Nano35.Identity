using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Identity.Api.Requests.CreateUser
{
    public class CreateUserUseCase : UseCaseEndPointNodeBase<ICreateUserRequestContract, ICreateUserResultContract>
    {
        private readonly IBus _bus;
        public CreateUserUseCase(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<ICreateUserResultContract>> Ask(ICreateUserRequestContract input) => 
            await new MasstransitUseCaseRequest<ICreateUserRequestContract, ICreateUserResultContract>(_bus, input)
                .GetResponse();
    }
}
