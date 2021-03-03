using System.Linq;
using FluentValidation;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.CreateUser
{
    public class ValidatedCreateUserRequestErrorResult :
        ICreateUserErrorResultContract
    {
        public string Error { get; set; }
    }
    
    public class ValidatedCreateUserRequest:
        IPipelineNode<
            ICreateUserRequestContract,
            ICreateUserResultContract>
    {
        private readonly IValidator<ICreateUserRequestContract> _validator;
        
        private readonly IPipelineNode<
            ICreateUserRequestContract, 
            ICreateUserResultContract> _nextNode;
        
        public ValidatedCreateUserRequest(
            IValidator<ICreateUserRequestContract> validator,
            IPipelineNode<
                ICreateUserRequestContract, 
                ICreateUserResultContract> nextNode)
        {   
            _validator = validator;
            _nextNode = nextNode;
            
        }

        public async Task<ICreateUserResultContract> Ask(
            ICreateUserRequestContract input)
        {
            var result = await _validator.ValidateAsync(input);

            if (!result.IsValid)
            {
                return new ValidatedCreateUserRequestErrorResult()
                    { Error = result.Errors.FirstOrDefault()?.ErrorMessage };
            }
            return await _nextNode.Ask(input);
        }
    }
}