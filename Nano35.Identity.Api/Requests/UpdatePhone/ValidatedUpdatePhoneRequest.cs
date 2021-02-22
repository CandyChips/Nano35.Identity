using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.UpdatePhone
{
    public class ValidatedUpdatePhoneRequestErrorResult :
        IUpdatePhoneErrorResultContract
    {
        public string Error { get; set; }
    }
    
    public class ValidatedUpdatePhoneRequest:
        IPipelineNode<
            IUpdatePhoneRequestContract,
            IUpdatePhoneResultContract>
    {
        private readonly IValidator<IUpdatePhoneRequestContract> _validator;
        
        private readonly IPipelineNode<
            IUpdatePhoneRequestContract, 
            IUpdatePhoneResultContract> _nextNode;

        public ValidatedUpdatePhoneRequest(
            IValidator<IUpdatePhoneRequestContract> validator,
            IPipelineNode<
                IUpdatePhoneRequestContract,
                IUpdatePhoneResultContract> nextNode)
        {
            _validator = validator;
            _nextNode = nextNode;
        }

        public async Task<IUpdatePhoneResultContract> Ask(
            IUpdatePhoneRequestContract input)
        {
            var result = await _validator.ValidateAsync(input);
            
            if (!result.IsValid)
            {
                return new ValidatedUpdatePhoneRequestErrorResult() 
                    {Error = result.Errors.FirstOrDefault()?.ErrorMessage};
            }
            return await _nextNode.Ask(input);
        }
    }
}