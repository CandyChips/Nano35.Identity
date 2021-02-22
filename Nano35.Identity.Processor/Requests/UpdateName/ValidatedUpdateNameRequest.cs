using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Processor.Requests.UpdateName
{
    public class ValidatedUpdateNameRequestErrorResult : 
        IUpdateNameErrorResultContract
    {
        public string Error { get; set; }
    }
    
    public class ValidatedUpdateNameRequest:
        IPipelineNode<
            IUpdateNameRequestContract,
            IUpdateNameResultContract>
    {
        private readonly IPipelineNode<
            IUpdateNameRequestContract, 
            IUpdateNameResultContract> _nextNode;

        public ValidatedUpdateNameRequest(
            IPipelineNode<
                IUpdateNameRequestContract,
                IUpdateNameResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdateNameResultContract> Ask(
            IUpdateNameRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new ValidatedUpdateNameRequestErrorResult() {Error = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}