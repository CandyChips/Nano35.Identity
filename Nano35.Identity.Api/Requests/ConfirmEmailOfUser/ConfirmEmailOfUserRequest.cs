using MassTransit;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.ConfirmEmailOfUser
{
    public class ConfirmEmailOfUserRequest : 
        MasstransitRequest
        <IConfirmEmailOfUserRequestContract, 
            IConfirmEmailOfUserResultContract,
            IConfirmEmailOfUserSuccessResultContract, 
            IConfirmEmailOfUserErrorResultContract>
    {
        public ConfirmEmailOfUserRequest(IBus bus, IConfirmEmailOfUserRequestContract request) : base(bus, request) {}
    }
}