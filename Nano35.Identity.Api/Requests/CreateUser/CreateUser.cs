using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Identity.Api.Requests.CreateUser
{
    public class CreateUser : EndPointNodeBase<ICreateUserRequestContract, ICreateUserResultContract>
    {
        private readonly IBus _bus;
        public CreateUser(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<ICreateUserResultContract>> Ask(ICreateUserRequestContract input) => 
            await new MasstransitRequest<ICreateUserRequestContract, ICreateUserResultContract>(_bus, input).GetResponse();
    }
}
