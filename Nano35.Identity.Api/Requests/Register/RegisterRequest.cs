using MassTransit;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.Register
{
    public class RegisterRequest : 
        MasstransitRequest
        <IRegisterRequestContract, 
            IRegisterResultContract,
            IRegisterSuccessResultContract, 
            IRegisterErrorResultContract>
    {
        public RegisterRequest(IBus bus, IRegisterRequestContract request) : base(bus, request) {}
    }
}