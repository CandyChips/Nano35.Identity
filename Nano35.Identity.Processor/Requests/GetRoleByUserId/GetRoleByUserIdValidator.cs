using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Processor.Requests.GetRoleByUserId
{
    public class GetRoleByUserIdValidatorErrorResult : IGetRoleByUserIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class GetRoleByUserIdValidator:
        IPipelineNode<IGetRoleByUserIdRequestContract, IGetRoleByUserIdResultContract>
    {
        private readonly IPipelineNode<IGetRoleByUserIdRequestContract, IGetRoleByUserIdResultContract> _nextNode;

        public GetRoleByUserIdValidator(
            IPipelineNode<IGetRoleByUserIdRequestContract, IGetRoleByUserIdResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetRoleByUserIdResultContract> Ask(IGetRoleByUserIdRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetRoleByUserIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}