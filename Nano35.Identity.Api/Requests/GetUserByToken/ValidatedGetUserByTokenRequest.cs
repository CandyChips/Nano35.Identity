using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Api.Requests.GenerateToken;

namespace Nano35.Identity.Api.Requests.GetUserByToken
{
    public class ValidatedGetUserByTokenRequestErrorResult :
        IGetUserByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetUserByTokenRequest:
        IPipelineNode<
            IGetUserByIdRequestContract,
            IGetUserByIdResultContract>
    {
        private readonly IValidator<IGetUserByIdRequestContract> _validator;
        private readonly IPipelineNode<
            IGetUserByIdRequestContract, 
            IGetUserByIdResultContract> _nextNode;

        public ValidatedGetUserByTokenRequest(
            IValidator<IGetUserByIdRequestContract> validator,
            IPipelineNode<
                IGetUserByIdRequestContract,
                IGetUserByIdResultContract> nextNode)
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
                return new ValidatedGetUserByTokenRequestErrorResult() 
                    {Message = result.Errors.FirstOrDefault()?.ErrorMessage};
            }
            return await _nextNode.Ask(input);
        }
    }
}