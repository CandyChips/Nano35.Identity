using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Processor.Requests.UpdateEmail
{
    public class ValidatedUpdateEmailRequestErrorResult : 
        IUpdateEmailErrorResultContract
    {
        public string Error { get; set; }
    }
    
    public class ValidatedUpdateEmailRequest:
        IPipelineNode<
            IUpdateEmailRequestContract,
            IUpdateEmailResultContract>
    {
        private readonly IPipelineNode<
            IUpdateEmailRequestContract, 
            IUpdateEmailResultContract> _nextNode;

        public ValidatedUpdateEmailRequest(
            IPipelineNode<
                IUpdateEmailRequestContract,
                IUpdateEmailResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdateEmailResultContract> Ask(
            IUpdateEmailRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new ValidatedUpdateEmailRequestErrorResult() {Error = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}