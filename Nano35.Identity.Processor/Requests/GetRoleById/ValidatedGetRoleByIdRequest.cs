using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Processor.Requests.GetRoleById
{
    public class ValidatedGetRoleByIdRequestErrorResult : IGetRoleByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetRoleByIdRequest:
        IPipelineNode<IGetRoleByIdRequestContract, IGetRoleByIdResultContract>
    {
        private readonly IPipelineNode<IGetRoleByIdRequestContract, IGetRoleByIdResultContract> _nextNode;

        public ValidatedGetRoleByIdRequest(
            IPipelineNode<IGetRoleByIdRequestContract, IGetRoleByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetRoleByIdResultContract> Ask(IGetRoleByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new ValidatedGetRoleByIdRequestErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}