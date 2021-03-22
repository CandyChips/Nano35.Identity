using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Identity.Artifacts;

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
            return (await request.GetResponse(input));
        }
    }
}
