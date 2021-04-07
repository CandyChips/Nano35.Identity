using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Nano35.Contracts;

namespace Nano35.Identity.Api.Requests
{
    public class Error : IError, IResponse
    {
        public string Message { get; set; }
    }
    
    public class ValidatedPipeNode<TIn, TOut> : PipeNodeBase<TIn, TOut>
        where TIn : IRequest
        where TOut : IResponse
    {
        private readonly IValidator<TIn> _validator;

        public ValidatedPipeNode(
            IValidator<TIn> validator,
            IPipeNode<TIn, TOut> next) : base(next)
        {
            _validator = validator;
        }

        public override async Task<TOut> Ask(TIn input)
        {
            var result = _validator.ValidateAsync(input).Result;
            if (!result.IsValid)
            {
                return (TOut) (IResponse) new Error() {Message = result.Errors.FirstOrDefault()?.ErrorMessage};
            }
            return await DoNext(input);
        }
    }
}