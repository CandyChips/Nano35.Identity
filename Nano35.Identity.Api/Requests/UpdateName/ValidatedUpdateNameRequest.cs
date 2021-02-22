using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.UpdateName
{
    public class ValidatedUpdateNameRequestErrorResult :
        IUpdateNameErrorResultContract
    {
        public string Error { get; set; }
    }
    
    public class ValidatedUpdateNameRequest:
        IPipelineNode<
            IUpdateNameRequestContract,
            IUpdateNameResultContract>
    {
        private readonly IValidator<IUpdateNameRequestContract> _validator;
        
        private readonly IPipelineNode<
            IUpdateNameRequestContract, 
            IUpdateNameResultContract> _nextNode;

        public ValidatedUpdateNameRequest(
            IValidator<IUpdateNameRequestContract> validator,
            IPipelineNode<
                IUpdateNameRequestContract,
                IUpdateNameResultContract> nextNode)
        {
            _validator = validator;
            _nextNode = nextNode;
        }

        public async Task<IUpdateNameResultContract> Ask(
            IUpdateNameRequestContract input)
        {
            var result = await _validator.ValidateAsync(input);
            
            if (!result.IsValid)
            {
                return new ValidatedUpdateNameRequestErrorResult() 
                    {Error = result.Errors.FirstOrDefault()?.ErrorMessage};
            }
            return await _nextNode.Ask(input);
        }
    }
}