using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Api.Helpers;

namespace Nano35.Identity.Api.Requests.Register
{
    public class RegisterUseCase :
        EndPointNodeBase<IRegisterRequestContract, IRegisterResultContract>
    {
        private readonly IBus _bus;

        public RegisterUseCase(IBus bus)
        {
            _bus = bus;
        }

        public override async Task<IRegisterResultContract> Ask(IRegisterRequestContract input)
        {
            input.Phone = PhoneConverter.RuPhoneConverter(input.Phone);
            var request = new RegisterRequest(_bus, input);
            return (await request.GetResponse(input));
        }
    }
}
