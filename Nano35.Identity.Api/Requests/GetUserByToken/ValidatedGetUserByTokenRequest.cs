using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GetUserByToken
{
    public class ValidatedGetUserByTokenRequestErrorResult :
        IGetUserByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetUserByTokenRequest:
        PipeNodeBase<IGetUserByIdRequestContract, IGetUserByIdResultContract>
    {
        private readonly IValidator<IGetUserByIdRequestContract> _validator;

        public ValidatedGetUserByTokenRequest(
            IValidator<IGetUserByIdRequestContract> validator,
            IPipeNode<IGetUserByIdRequestContract, IGetUserByIdResultContract> next) :
            base(next)
        {
            _validator = validator;
        }

        public override Task<IGetUserByIdResultContract> Ask(IGetUserByIdRequestContract input)
        {
            var result =  _validator.ValidateAsync(input).Result;

            if (!result.IsValid)
            {
                return Task.Run(() => new ValidatedGetUserByTokenRequestErrorResult()
                    {Message = result.Errors.FirstOrDefault()?.ErrorMessage} as IGetUserByIdResultContract);
            }
            return DoNext(input);
        }
    }
}