using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GenerateToken
{
    public class ValidatedGenerateTokenRequestErrorResult : IGenerateTokenErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGenerateTokenRequest:
        IPipelineNode<IGenerateTokenRequestContract, IGenerateTokenResultContract>
    {
        private readonly IPipelineNode<IGenerateTokenRequestContract, IGenerateTokenResultContract> _nextNode;

        public ValidatedGenerateTokenRequest(
            IPipelineNode<IGenerateTokenRequestContract, IGenerateTokenResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGenerateTokenResultContract> Ask(
            IGenerateTokenRequestContract input)
        {
            if (false)
            {
                return new ValidatedGenerateTokenRequestErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}