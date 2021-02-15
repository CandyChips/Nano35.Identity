using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Processor.Requests.GetAllUsers
{
    public class ValidatedGetAllUsersRequestErrorResult : IGetAllUsersErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllUsersRequest:
        IPipelineNode<IGetAllUsersRequestContract, IGetAllUsersResultContract>
    {
        private readonly IPipelineNode<IGetAllUsersRequestContract, IGetAllUsersResultContract> _nextNode;

        public ValidatedGetAllUsersRequest(
            IPipelineNode<IGetAllUsersRequestContract, IGetAllUsersResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllUsersResultContract> Ask(IGetAllUsersRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new ValidatedGetAllUsersRequestErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}