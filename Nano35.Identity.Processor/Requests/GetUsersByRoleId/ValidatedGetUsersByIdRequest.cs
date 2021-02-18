using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Processor.Requests.GetUserByRoleId
{
    public class ValidatedGetUserByRoleIdRequestErrorResult : IGetUsersByRoleIdNotFoundResultContract
    {
        public string Message { get; set; }
    }
    
    public class GetUserByRoleIdValidator:
        IPipelineNode<IGetUsersByRoleIdRequestContract, IGetUsersByRoleIdResultContract>
    {
        private readonly IPipelineNode<IGetUsersByRoleIdRequestContract, IGetUsersByRoleIdResultContract> _nextNode;

        public GetUserByRoleIdValidator(
            IPipelineNode<IGetUsersByRoleIdRequestContract, IGetUsersByRoleIdResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetUsersByRoleIdResultContract> Ask(IGetUsersByRoleIdRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new ValidatedGetUserByRoleIdRequestErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}