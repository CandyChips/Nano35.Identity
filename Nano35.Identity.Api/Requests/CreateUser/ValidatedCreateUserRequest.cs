using System.Linq;
using FluentValidation;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.CreateUser
{
    public class ValidatedCreateUserRequestErrorResult :
        ICreateUserErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedCreateUserRequest:
        PipeNodeBase<ICreateUserRequestContract, ICreateUserResultContract>
    {
        private readonly IValidator<ICreateUserRequestContract> _validator;
        
        public ValidatedCreateUserRequest(
            IValidator<ICreateUserRequestContract> validator,
            IPipeNode<ICreateUserRequestContract, ICreateUserResultContract> next) :
            base(next)
        {   
            _validator = validator;
        }

        public override Task<ICreateUserResultContract> Ask(ICreateUserRequestContract input)
        {
            var result = _validator.ValidateAsync(input).Result;

            if (!result.IsValid)
            {
                return Task.Run(() => new ValidatedCreateUserRequestErrorResult()
                    {Message = result.Errors.FirstOrDefault()?.ErrorMessage} as ICreateUserResultContract);
            }
            return DoNext(input);
        }
    }
}