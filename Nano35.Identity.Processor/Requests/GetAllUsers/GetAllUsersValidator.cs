using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Processor.Requests.GetAllUsers
{
    public class GetAllUsersValidatorErrorResult : IGetAllUsersErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class GetAllUsersValidator:
        IPipelineNode<IGetAllUsersRequestContract, IGetAllUsersResultContract>
    {
        private readonly IPipelineNode<IGetAllUsersRequestContract, IGetAllUsersResultContract> _nextNode;

        public GetAllUsersValidator(
            IPipelineNode<IGetAllUsersRequestContract, IGetAllUsersResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllUsersResultContract> Ask(IGetAllUsersRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllUsersValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}