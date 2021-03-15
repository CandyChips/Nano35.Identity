using MassTransit;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.CreateUser
{
    public class CreateUserRequest : 
        MasstransitRequest
        <ICreateUserRequestContract, 
            ICreateUserResultContract,
            ICreateUserSuccessResultContract, 
            ICreateUserErrorResultContract>
    {
        public CreateUserRequest(IBus bus, ICreateUserRequestContract request) : base(bus, request) {}
    }
}