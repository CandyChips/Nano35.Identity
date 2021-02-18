using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Processor.Requests.UpdatePassword
{
    public class ValidatedUpdatePasswordRequestErrorResult : IUpdatePasswordErrorResultContract
    {
        public string Error { get; set; }
    }
    
    public class UpdatePasswordValidator:
        IPipelineNode<IUpdatePasswordRequestContract, IUpdatePasswordResultContract>
    {
        private readonly IPipelineNode<IUpdatePasswordRequestContract, IUpdatePasswordResultContract> _nextNode;

        public UpdatePasswordValidator(
            IPipelineNode<IUpdatePasswordRequestContract, IUpdatePasswordResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdatePasswordResultContract> Ask(IUpdatePasswordRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new ValidatedUpdatePasswordRequestErrorResult() {Error = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}