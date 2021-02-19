using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.UpdatePhone
{
    public class ValidatedUpdatePhoneRequestErrorResult :
        IUpdatePhoneErrorResultContract
    {
        public string Error { get; set; }
    }
    
    public class ValidatedUpdatePhoneRequest:
        IPipelineNode<
            IUpdatePhoneRequestContract,
            IUpdatePhoneResultContract>
    {
        private readonly IPipelineNode<
            IUpdatePhoneRequestContract, 
            IUpdatePhoneResultContract> _nextNode;

        public ValidatedUpdatePhoneRequest(
            IPipelineNode<
                IUpdatePhoneRequestContract,
                IUpdatePhoneResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdatePhoneResultContract> Ask(
            IUpdatePhoneRequestContract input)
        {
            if (false)
            {
                return new ValidatedUpdatePhoneRequestErrorResult() {Error = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}