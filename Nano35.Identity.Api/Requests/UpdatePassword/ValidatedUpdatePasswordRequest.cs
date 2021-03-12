using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.UpdatePassword
{
    public class ValidatedUpdatePasswordRequestErrorResult :
     IUpdatePasswordErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdatePasswordRequest:
        PipeNodeBase<IUpdatePasswordRequestContract, IUpdatePasswordResultContract>
    {
        private readonly IValidator<IUpdatePasswordRequestContract> _validator;

        public ValidatedUpdatePasswordRequest(
            IValidator<IUpdatePasswordRequestContract> validator,
            IPipeNode<IUpdatePasswordRequestContract, IUpdatePasswordResultContract> next) :
            base(next)
        {
            _validator = validator;
        }

        public override Task<IUpdatePasswordResultContract> Ask(IUpdatePasswordRequestContract input)
        {
            var result = _validator.ValidateAsync(input).Result;

            if (!result.IsValid)
            {
                return Task.Run(() => new ValidatedUpdatePasswordRequestErrorResult()
                    {Message = result.Errors.FirstOrDefault()?.ErrorMessage} as IUpdatePasswordResultContract);
            }
            
            return DoNext(input);
        }
    }
}