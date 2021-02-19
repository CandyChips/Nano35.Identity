using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GetRoleById
{
    public class ValidatedGetRoleByIdRequestErrorResult :
        IGetRoleByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetRoleByIdRequest:
        IPipelineNode<
            IGetRoleByIdRequestContract, 
            IGetRoleByIdResultContract>
    {
        private readonly IValidator<IGetRoleByIdRequestContract> _validator;
        
        private readonly IPipelineNode<
            IGetRoleByIdRequestContract, 
            IGetRoleByIdResultContract> _nextNode;

        public ValidatedGetRoleByIdRequest(
            IValidator<IGetRoleByIdRequestContract> validator,
            IPipelineNode<
                IGetRoleByIdRequestContract,
                IGetRoleByIdResultContract> nextNode)
        {
            _validator = validator;
            _nextNode = nextNode;
        }

        public async Task<IGetRoleByIdResultContract> Ask(
            IGetRoleByIdRequestContract input)
        {
            var result = await _validator.ValidateAsync(input);
            
            if (!result.IsValid)
            {
                return new ValidatedGetRoleByIdRequestErrorResult() 
                    {Message = result.Errors.FirstOrDefault()?.ErrorMessage};
            }
            return await _nextNode.Ask(input);
        }
    }
}