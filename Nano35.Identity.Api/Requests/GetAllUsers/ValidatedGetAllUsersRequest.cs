using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GetAllUsers
{
    public class ValidatedGetAllUsersRequestErrorResult : 
        IGetAllUsersErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllUsersRequest:
        PipeNodeBase<IGetAllUsersRequestContract, IGetAllUsersResultContract>
    {
        public ValidatedGetAllUsersRequest(
            IPipeNode<IGetAllUsersRequestContract, IGetAllUsersResultContract> next) :
            base(next)
        {
            
        }

        public override Task<IGetAllUsersResultContract> Ask(IGetAllUsersRequestContract input)
        {
            return DoNext(input);
        }
    }
}