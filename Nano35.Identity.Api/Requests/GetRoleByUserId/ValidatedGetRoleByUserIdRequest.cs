using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GetRoleByUserId
{
    public class ValidatedGetRoleByUserIdRequestErrorResult : 
        IGetRoleByUserIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetRoleByUserIdRequest:
        IPipelineNode<
            IGetRoleByUserIdRequestContract,
            IGetRoleByUserIdResultContract>
    {
        private readonly IValidator<IGetRoleByUserIdRequestContract> _validator;
        
        private readonly IPipelineNode<
            IGetRoleByUserIdRequestContract, 
            IGetRoleByUserIdResultContract> _nextNode;

        public ValidatedGetRoleByUserIdRequest(
            IValidator<IGetRoleByUserIdRequestContract> validator,
            IPipelineNode<
                IGetRoleByUserIdRequestContract,
                IGetRoleByUserIdResultContract> nextNode)
        {
            _validator = validator;
            _nextNode = nextNode;
        }

        public async Task<IGetRoleByUserIdResultContract> Ask(
            IGetRoleByUserIdRequestContract input)
        {
            var result = await _validator.ValidateAsync(input);
            if (!result.IsValid)
            {
                return new ValidatedGetRoleByUserIdRequestErrorResult() 
                    {Message = result.Errors.FirstOrDefault()?.ErrorMessage};
            }
            return await _nextNode.Ask(input);
        }
    }
}