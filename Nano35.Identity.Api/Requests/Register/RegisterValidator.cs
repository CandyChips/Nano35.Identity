using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.Register
{
    public class RegisterValidatorErrorResult : IRegisterErrorResultContract
    {
        public string Error { get; set; }
    }
    
    public class RegisterValidator:
        IPipelineNode<IRegisterRequestContract, IRegisterResultContract>
    {
        private readonly IPipelineNode<IRegisterRequestContract, IRegisterResultContract> _nextNode;

        public RegisterValidator(
            IPipelineNode<IRegisterRequestContract, IRegisterResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IRegisterResultContract> Ask(
            IRegisterRequestContract input)
        {
            if (false)
            {
                return new RegisterValidatorErrorResult() {Error = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}