using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.UpdatePassword
{
    public class ValidatedUpdatePasswordRequestErrorResult :
     IUpdatePasswordErrorResultContract
    {
        public string Error { get; set; }
    }
    
    public class ValidatedUpdatePasswordRequest:
        IPipelineNode<
            IUpdatePasswordRequestContract,
            IUpdatePasswordResultContract>
    {
        private readonly IValidator<IUpdatePasswordRequestContract> _validator;

        private readonly IPipelineNode<
            IUpdatePasswordRequestContract, 
            IUpdatePasswordResultContract> _nextNode;

        public ValidatedUpdatePasswordRequest(
            IValidator<IUpdatePasswordRequestContract> validator,
            IPipelineNode<
                IUpdatePasswordRequestContract, 
                IUpdatePasswordResultContract> nextNode)
        {
            _nextNode = nextNode;
            _validator = validator;
        }

        public async Task<IUpdatePasswordResultContract> Ask(
            IUpdatePasswordRequestContract input)
        {
            var result = await _validator.ValidateAsync(input);
            
            if (!result.IsValid)
            {
                return new ValidatedUpdatePasswordRequestErrorResult() 
                    {Error = result.Errors.FirstOrDefault()?.ErrorMessage};
            }
            return await _nextNode.Ask(input);
        }
    }
}