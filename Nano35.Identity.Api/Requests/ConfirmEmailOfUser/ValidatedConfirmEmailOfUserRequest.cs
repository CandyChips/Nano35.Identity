using System.Linq;
using FluentValidation;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.ConfirmEmailOfUser
{
    public class ValidatedConfirmEmailOfUserRequestErrorResult :
        IConfirmEmailOfUserErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedConfirmEmailOfUserRequest:
        PipeNodeBase<IConfirmEmailOfUserRequestContract, IConfirmEmailOfUserResultContract>
    {
        private readonly IValidator<IConfirmEmailOfUserRequestContract> _validator;
        
        public ValidatedConfirmEmailOfUserRequest(
            IValidator<IConfirmEmailOfUserRequestContract> validator,
            IPipeNode<IConfirmEmailOfUserRequestContract, IConfirmEmailOfUserResultContract> next) :
            base(next)
        {   
            _validator = validator;
        }

        public override Task<IConfirmEmailOfUserResultContract> Ask(IConfirmEmailOfUserRequestContract input)
        {
            var result = _validator.ValidateAsync(input).Result;

            if (!result.IsValid)
            {
                return Task.Run(() => new ValidatedConfirmEmailOfUserRequestErrorResult()
                    {Message = result.Errors.FirstOrDefault()?.ErrorMessage} as IConfirmEmailOfUserResultContract);
            }

            return DoNext(input);
        }
    }
}