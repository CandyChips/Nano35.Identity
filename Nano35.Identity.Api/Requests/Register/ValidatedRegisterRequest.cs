using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.Register
{
    public class ValidatedRegisterRequestErrorResult : 
        IRegisterErrorResultContract
    {
        public string Error { get; set; }
        public string Message { get; set; }
    }
    
    public class ValidatedRegisterRequest:
        PipeNodeBase<IRegisterRequestContract, IRegisterResultContract>
    {
        private readonly IValidator<IRegisterRequestContract> _validator;

        public ValidatedRegisterRequest(
            IValidator<IRegisterRequestContract> validator,
            IPipeNode<IRegisterRequestContract, IRegisterResultContract> next) :
            base(next)
        {
            _validator = validator;
        }

        public override async Task<IRegisterResultContract> Ask(IRegisterRequestContract input)
        {
            var result = _validator.ValidateAsync(input).Result;
            
            if (!result.IsValid)
            {
                return new ValidatedRegisterRequestErrorResult() 
                    {Error = result.Errors.FirstOrDefault()?.ErrorMessage};
            }
            return await DoNext(input);
        }
    }
}