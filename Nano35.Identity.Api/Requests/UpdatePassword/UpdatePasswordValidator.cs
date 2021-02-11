using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.UpdatePassword
{
    public class UpdatePasswordValidatorErrorResult : IUpdatePasswordErrorResultContract
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

        public async Task<IUpdatePasswordResultContract> Ask(
            IUpdatePasswordRequestContract input)
        {
            if (false)
            {
                return new UpdatePasswordValidatorErrorResult() {Error = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}