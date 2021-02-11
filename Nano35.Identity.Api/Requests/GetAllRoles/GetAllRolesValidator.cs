using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GetAllRoles
{
    public class GetAllRolesValidatorErrorResult : IGetAllRolesErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class GetAllRolesValidator:
        IPipelineNode<IGetAllRolesRequestContract, IGetAllRolesResultContract>
    {
        private readonly IPipelineNode<IGetAllRolesRequestContract, IGetAllRolesResultContract> _nextNode;

        public GetAllRolesValidator(
            IPipelineNode<IGetAllRolesRequestContract, IGetAllRolesResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllRolesResultContract> Ask(
            IGetAllRolesRequestContract input)
        {
            if (false)
            {
                return new GetAllRolesValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}