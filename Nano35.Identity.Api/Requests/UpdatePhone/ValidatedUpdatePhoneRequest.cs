using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.UpdatePhone
{
    public class ValidatedUpdatePhoneRequestErrorResult :
        IUpdatePhoneErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdatePhoneRequest:
        PipeNodeBase<IUpdatePhoneRequestContract, IUpdatePhoneResultContract>
    {
        private readonly IValidator<IUpdatePhoneRequestContract> _validator;
        
        public ValidatedUpdatePhoneRequest(
            IValidator<IUpdatePhoneRequestContract> validator,
            IPipeNode<IUpdatePhoneRequestContract, IUpdatePhoneResultContract> next) :
            base(next)
        {
            _validator = validator;
        }

        public override Task<IUpdatePhoneResultContract> Ask(IUpdatePhoneRequestContract input)
        {
            var result = _validator.ValidateAsync(input).Result;
            
            if (!result.IsValid)
            {
                return Task.Run(() => new ValidatedUpdatePhoneRequestErrorResult()
                    {Message = result.Errors.FirstOrDefault()?.ErrorMessage} as IUpdatePhoneResultContract);
            }
            
            return DoNext(input);
        }
    }
}