using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GetUserById
{
    public class ValidatedGetUserByIdRequestErrorResult : IGetUserByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetUserByIdRequest:
        IPipelineNode<IGetUserByIdRequestContract, IGetUserByIdResultContract>
    {
        private readonly IValidator<IGetUserByIdRequestContract> _validator;
        private readonly IPipelineNode<IGetUserByIdRequestContract, IGetUserByIdResultContract> _nextNode;

        public ValidatedGetUserByIdRequest(
            IValidator<IGetUserByIdRequestContract> validator,
            IPipelineNode<IGetUserByIdRequestContract, IGetUserByIdResultContract> nextNode)
        {
            _validator = validator;
            _nextNode = nextNode;
        }

        public async Task<IGetUserByIdResultContract> Ask(
            IGetUserByIdRequestContract input)
        {
            var result = await _validator.ValidateAsync(input);
            if (!result.IsValid)
            {
                return new ValidatedGetUserByIdRequestErrorResult() 
                    {Message = result.Errors.FirstOrDefault()?.ErrorMessage};
            }
            return await _nextNode.Ask(input);
        }
    }
    public class GetUserByIdRequestValidator :
        AbstractValidator<IGetUserByIdRequestContract> 
    {
        public GetUserByIdRequestValidator()
        {
            RuleFor(id => id.UserId).NotEmpty().WithMessage("нет userId");
        }
    } 
}