using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GetUsersByRoleId
{
    public class ValidatedGetUsersByRoleIdRequestErrorResult : IGetUsersByRoleIdNotFoundResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetUsersByRoleIdRequest:
        IPipelineNode<IGetUsersByRoleIdRequestContract, IGetUsersByRoleIdResultContract>
    {
        private readonly IPipelineNode<IGetUsersByRoleIdRequestContract, IGetUsersByRoleIdResultContract> _nextNode;

        public ValidatedGetUsersByRoleIdRequest(
            IPipelineNode<IGetUsersByRoleIdRequestContract, IGetUsersByRoleIdResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetUsersByRoleIdResultContract> Ask(
            IGetUsersByRoleIdRequestContract input)
        {
            if (false)
            {
                return new ValidatedGetUsersByRoleIdRequestErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}