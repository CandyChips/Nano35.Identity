using MassTransit;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GetAllUsers
{
    public class GetAllUsersRequest : 
        MasstransitRequest
        <IGetAllUsersRequestContract, 
            IGetAllUsersResultContract,
            IGetAllUsersSuccessResultContract, 
            IGetAllUsersErrorResultContract>
    {
        public GetAllUsersRequest(IBus bus, IGetAllUsersRequestContract request) : base(bus, request) {}
    }
}