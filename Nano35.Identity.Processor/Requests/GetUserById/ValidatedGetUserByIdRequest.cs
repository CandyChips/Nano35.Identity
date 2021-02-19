using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Processor.Requests.GetUserById
{
    public class ValidatedGetUserByIdRequestErrorResult :
        IGetUserByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetUserByIdRequest:
        IPipelineNode<
            IGetUserByIdRequestContract, 
            IGetUserByIdResultContract>
    {
        private readonly IPipelineNode<
            IGetUserByIdRequestContract, 
            IGetUserByIdResultContract> _nextNode;

        public ValidatedGetUserByIdRequest(
            IPipelineNode<
                IGetUserByIdRequestContract,
                IGetUserByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetUserByIdResultContract> Ask(
            IGetUserByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new ValidatedGetUserByIdRequestErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}