using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.UpdateName
{
    public class ValidatedUpdateNameRequestErrorResult :
        IUpdateNameErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateNameRequest:
        PipeNodeBase<IUpdateNameRequestContract, IUpdateNameResultContract>
    {
        private readonly IValidator<IUpdateNameRequestContract> _validator;
        
        public ValidatedUpdateNameRequest(
            IValidator<IUpdateNameRequestContract> validator,
            IPipeNode<IUpdateNameRequestContract, IUpdateNameResultContract> next) :
            base(next)
        {
            _validator = validator;
        }

        public override Task<IUpdateNameResultContract> Ask(IUpdateNameRequestContract input)
        {
            var result = _validator.ValidateAsync(input).Result;
            
            if (!result.IsValid)
            {
                return Task.Run(() => new ValidatedUpdateNameRequestErrorResult()
                    {Message = result.Errors.FirstOrDefault()?.ErrorMessage} as IUpdateNameResultContract);
            }
            
            return DoNext(input);
        }
    }
}