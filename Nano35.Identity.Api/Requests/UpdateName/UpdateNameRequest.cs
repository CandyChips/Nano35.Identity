using MassTransit;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.UpdateName
{
    public class UpdateNameRequest : 
        MasstransitRequest
        <IUpdateNameRequestContract, 
            IUpdateNameResultContract,
            IUpdateNameSuccessResultContract, 
            IUpdateNameErrorResultContract>
    {
        public UpdateNameRequest(IBus bus, IUpdateNameRequestContract request) : base(bus, request) {}
    }
}