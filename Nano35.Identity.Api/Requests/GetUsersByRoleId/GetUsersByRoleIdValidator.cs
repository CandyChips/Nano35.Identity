using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GetUsersByRoleId
{
    public class GetUsersByRoleIdValidatorErrorResult : IGetUsersByRoleIdNotFoundResultContract
    {
        public string Message { get; set; }
    }
    
    public class GetUsersByRoleIdValidator:
        IPipelineNode<IGetUsersByRoleIdRequestContract, IGetUsersByRoleIdResultContract>
    {
        private readonly IPipelineNode<IGetUsersByRoleIdRequestContract, IGetUsersByRoleIdResultContract> _nextNode;

        public GetUsersByRoleIdValidator(
            IPipelineNode<IGetUsersByRoleIdRequestContract, IGetUsersByRoleIdResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetUsersByRoleIdResultContract> Ask(
            IGetUsersByRoleIdRequestContract input)
        {
            if (false)
            {
                return new GetUsersByRoleIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}