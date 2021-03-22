using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Api.Helpers;

namespace Nano35.Identity.Api.Requests.UpdatePhone
{
    public class UpdatePhoneUseCase :
        EndPointNodeBase<IUpdatePhoneRequestContract, IUpdatePhoneResultContract>
    {
        private readonly IBus _bus;

        public UpdatePhoneUseCase(IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IUpdatePhoneResultContract> Ask(IUpdatePhoneRequestContract input)
        {
            
            input.Phone = PhoneConverter.RuPhoneConverter(input.Phone);
            var request = new UpdatePhoneRequest(_bus, input);
            return (await request.GetResponse(input));
        }
    }
}
