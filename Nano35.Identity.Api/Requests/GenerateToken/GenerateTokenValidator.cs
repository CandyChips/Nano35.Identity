using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GenerateToken
{
    public class GenerateTokenValidatorErrorResult : IGenerateTokenErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class GenerateTokenValidator:
        IPipelineNode<IGenerateTokenRequestContract, IGenerateTokenResultContract>
    {
        private readonly IPipelineNode<IGenerateTokenRequestContract, IGenerateTokenResultContract> _nextNode;

        public GenerateTokenValidator(
            IPipelineNode<IGenerateTokenRequestContract, IGenerateTokenResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGenerateTokenResultContract> Ask(
            IGenerateTokenRequestContract input)
        {
            if (false)
            {
                return new GenerateTokenValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}