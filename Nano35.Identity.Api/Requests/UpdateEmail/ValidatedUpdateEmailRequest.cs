using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.UpdateEmail
{
    public class ValidatedUpdateEmailRequestErrorResult :
        IUpdateEmailErrorResultContract
    {
        public string Error { get; set; }
    }
    
    public class ValidatedUpdateEmailRequest:
        IPipelineNode<
            IUpdateEmailRequestContract,
            IUpdateEmailResultContract>
    {
        private readonly IValidator<IUpdateEmailRequestContract> _validator;
        
        private readonly IPipelineNode<
            IUpdateEmailRequestContract, 
            IUpdateEmailResultContract> _nextNode;

        public ValidatedUpdateEmailRequest(
            IValidator<IUpdateEmailRequestContract> validator,
            IPipelineNode<
                IUpdateEmailRequestContract,
                IUpdateEmailResultContract> nextNode)
        {
            _validator = validator;
            _nextNode = nextNode;
        }

        public async Task<IUpdateEmailResultContract> Ask(
            IUpdateEmailRequestContract input)
        {
            var result = await _validator.ValidateAsync(input);
            
            if (!result.IsValid)
            {
                return new ValidatedUpdateEmailRequestErrorResult() 
                    {Error = result.Errors.FirstOrDefault()?.ErrorMessage};
            }
            return await _nextNode.Ask(input);
        }
    }
}