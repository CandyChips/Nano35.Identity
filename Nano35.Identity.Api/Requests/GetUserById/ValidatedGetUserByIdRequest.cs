using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GetUserById
{
    public class ValidatedGetUserByIdRequestErrorResult : 
        IGetUserByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetUserByIdRequest:
        PipeNodeBase<IGetUserByIdRequestContract, IGetUserByIdResultContract>
    {
        private readonly IValidator<IGetUserByIdRequestContract> _validator;
        
        public ValidatedGetUserByIdRequest(
            IValidator<IGetUserByIdRequestContract> validator,
            IPipeNode<IGetUserByIdRequestContract, IGetUserByIdResultContract> next) :
            base(next)
        {
            _validator = validator;
        }

        public override Task<IGetUserByIdResultContract> Ask(IGetUserByIdRequestContract input)
        {
            var result = _validator.ValidateAsync(input).Result;
            if (!result.IsValid)
            {
                return Task.Run(() => new ValidatedGetUserByIdRequestErrorResult()
                    {Message = result.Errors.FirstOrDefault()?.ErrorMessage} as IGetUserByIdResultContract);
            }
            return DoNext(input);
        }
    }
}