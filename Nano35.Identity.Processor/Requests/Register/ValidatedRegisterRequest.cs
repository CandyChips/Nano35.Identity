using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Processor.Requests.Register
{
    public class ValidatedRegisterRequestErrorResult :
        IRegisterErrorResultContract
    {
        public string Error { get; set; }
    }
    
    public class ValidatedRegisterRequest:
        IPipelineNode<
            IRegisterRequestContract, 
            IRegisterResultContract>
    {
        private readonly IPipelineNode<
            IRegisterRequestContract, 
            IRegisterResultContract> _nextNode;

        public ValidatedRegisterRequest(
            IPipelineNode<
                IRegisterRequestContract,
                IRegisterResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IRegisterResultContract> Ask(IRegisterRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new ValidatedRegisterRequestErrorResult() {Error = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}