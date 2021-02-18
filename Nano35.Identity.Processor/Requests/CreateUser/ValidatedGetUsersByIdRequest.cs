using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Processor.Requests.CreateUser
{
    public class ValidatedCreateUserRequestErrorResult : ICreateUserErrorResultContract
    {
        public string Error { get; set; }
    }
    
    public class CreateUserValidator:
        IPipelineNode<ICreateUserRequestContract, ICreateUserResultContract>
    {
        private readonly IPipelineNode<ICreateUserRequestContract, ICreateUserResultContract> _nextNode;

        public CreateUserValidator(
            IPipelineNode<ICreateUserRequestContract, ICreateUserResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<ICreateUserResultContract> Ask(ICreateUserRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new ValidatedCreateUserRequestErrorResult() {Error = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}