using MassTransit;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.UpdatePhone
{
    public class UpdatePhoneRequest : 
        MasstransitRequest
        <IUpdatePhoneRequestContract, 
            IUpdatePhoneResultContract,
            IUpdatePhoneSuccessResultContract, 
            IUpdatePhoneErrorResultContract>
    {
        public UpdatePhoneRequest(IBus bus, IUpdatePhoneRequestContract request) : base(bus, request) {}
    }
}