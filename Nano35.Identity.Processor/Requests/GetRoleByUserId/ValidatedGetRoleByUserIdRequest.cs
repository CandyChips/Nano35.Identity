using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Processor.Requests.GetRoleByUserId
{
    public class ValidatedGetRoleByUserIdRequestErrorResult : IGetRoleByUserIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetRoleByUserIdRequest:
        IPipelineNode<IGetRoleByUserIdRequestContract, IGetRoleByUserIdResultContract>
    {
        private readonly IPipelineNode<IGetRoleByUserIdRequestContract, IGetRoleByUserIdResultContract> _nextNode;

        public ValidatedGetRoleByUserIdRequest(
            IPipelineNode<IGetRoleByUserIdRequestContract, IGetRoleByUserIdResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetRoleByUserIdResultContract> Ask(IGetRoleByUserIdRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new ValidatedGetRoleByUserIdRequestErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}