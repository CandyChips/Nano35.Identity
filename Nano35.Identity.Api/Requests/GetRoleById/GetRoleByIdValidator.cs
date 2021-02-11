using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GetRoleById
{
    public class GetRoleByIdValidatorErrorResult : IGetRoleByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class GetRoleByIdValidator:
        IPipelineNode<IGetRoleByIdRequestContract, IGetRoleByIdResultContract>
    {
        private readonly IPipelineNode<IGetRoleByIdRequestContract, IGetRoleByIdResultContract> _nextNode;

        public GetRoleByIdValidator(
            IPipelineNode<IGetRoleByIdRequestContract, IGetRoleByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetRoleByIdResultContract> Ask(
            IGetRoleByIdRequestContract input)
        {
            if (false)
            {
                return new GetRoleByIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}