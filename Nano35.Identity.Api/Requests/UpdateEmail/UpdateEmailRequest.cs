using MassTransit;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.UpdateEmail
{
    public class UpdateEmailRequest : 
        MasstransitRequest
        <IUpdateEmailRequestContract, 
            IUpdateEmailResultContract,
            IUpdateEmailSuccessResultContract, 
            IUpdateEmailErrorResultContract>
    {
        public UpdateEmailRequest(IBus bus, IUpdateEmailRequestContract request) : base(bus) {}
    }
}