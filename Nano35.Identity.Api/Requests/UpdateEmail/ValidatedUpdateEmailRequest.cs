using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.UpdateEmail
{
    public class ValidatedUpdateEmailRequestErrorResult :
        IUpdateEmailErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateEmailRequest:
        PipeNodeBase<IUpdateEmailRequestContract, IUpdateEmailResultContract>
    {
        private readonly IValidator<IUpdateEmailRequestContract> _validator;

        public ValidatedUpdateEmailRequest(
            IValidator<IUpdateEmailRequestContract> validator,
            IPipeNode<IUpdateEmailRequestContract, IUpdateEmailResultContract> next) :
            base(next)
        {
            _validator = validator;
        }

        public override Task<IUpdateEmailResultContract> Ask(IUpdateEmailRequestContract input)
        {
            var result = _validator.ValidateAsync(input).Result;
            
            if (!result.IsValid)
            {
                return Task.Run(() => new ValidatedUpdateEmailRequestErrorResult()
                    {Message = result.Errors.FirstOrDefault()?.ErrorMessage} as IUpdateEmailResultContract);
            }
            
            return DoNext(input);
        }
    }
}