using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GetUsersByRoleId
{
    public class ValidatedGetUsersByRoleIdRequestErrorResult : IGetUsersByRoleIdNotFoundResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetUsersByRoleIdRequest:
        IPipelineNode<IGetUsersByRoleIdRequestContract, IGetUsersByRoleIdResultContract>
    {
        private readonly IValidator<IGetUsersByRoleIdRequestContract> _validator;
        private readonly IPipelineNode<IGetUsersByRoleIdRequestContract, IGetUsersByRoleIdResultContract> _nextNode;

        public ValidatedGetUsersByRoleIdRequest(
            IValidator<IGetUsersByRoleIdRequestContract> validator,
            IPipelineNode<IGetUsersByRoleIdRequestContract, IGetUsersByRoleIdResultContract> nextNode)
        {
            _validator = validator;
            _nextNode = nextNode;
        }

        public async Task<IGetUsersByRoleIdResultContract> Ask(
            IGetUsersByRoleIdRequestContract input)
        {
            var result = await _validator.ValidateAsync(input);

            if (!result.IsValid)
            {
                return new ValidatedGetUsersByRoleIdRequestErrorResult() 
                    {Message = result.Errors.FirstOrDefault()?.ErrorMessage};
            }
            return await _nextNode.Ask(input);
        }
    }

    public class GetUsersByRoleIdRequestValidator :
        AbstractValidator<IGetUsersByRoleIdRequestContract>
    {
        public GetUsersByRoleIdRequestValidator()
        {
            RuleFor(id => id.Id).NotEmpty().WithMessage("нет id");
        }
    }
}