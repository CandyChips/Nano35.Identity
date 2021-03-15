using MassTransit;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GetUserById
{
    public class GetUserByIdRequest : 
        MasstransitRequest
        <IGetUserByIdRequestContract, 
            IGetUserByIdResultContract,
            IGetUserByIdSuccessResultContract, 
            IGetUserByIdErrorResultContract>
    {
        public GetUserByIdRequest(IBus bus, IGetUserByIdRequestContract request) : base(bus, request) {}
    }
}