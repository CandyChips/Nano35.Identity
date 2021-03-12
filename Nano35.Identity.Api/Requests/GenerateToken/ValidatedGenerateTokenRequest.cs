using System.Linq;
using FluentValidation;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GenerateToken
{
    public class ValidatedGenerateTokenRequestErrorResult :
        IGenerateTokenErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGenerateTokenRequest:
        PipeNodeBase<IGenerateTokenRequestContract, IGenerateTokenResultContract>
    {
        private readonly IValidator<IGenerateTokenRequestContract> _validator;
        
        public ValidatedGenerateTokenRequest(
            IValidator<IGenerateTokenRequestContract> validator,
            IPipeNode<IGenerateTokenRequestContract, IGenerateTokenResultContract> next) :
            base(next)
        {   
            _validator = validator;
        }

        public override Task<IGenerateTokenResultContract> Ask(IGenerateTokenRequestContract input)
        {
            var result =  _validator.ValidateAsync(input).Result;

            if (!result.IsValid)
            {
                return Task.Run(() => new ValidatedGenerateTokenRequestErrorResult()
                    {Message = result.Errors.FirstOrDefault()?.ErrorMessage} as IGenerateTokenResultContract);
            }
            return DoNext(input);
        }
    }
}