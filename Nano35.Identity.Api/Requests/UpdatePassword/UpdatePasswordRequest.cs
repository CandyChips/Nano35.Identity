using MassTransit;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.UpdatePassword
{
    public class UpdatePasswordRequest : 
        MasstransitRequest
        <IUpdatePasswordRequestContract, 
            IUpdatePasswordResultContract,
            IUpdatePasswordSuccessResultContract, 
            IUpdatePasswordErrorResultContract>
    {
        public UpdatePasswordRequest(IBus bus, IUpdatePasswordRequestContract request) : base(bus) {}
    }
}