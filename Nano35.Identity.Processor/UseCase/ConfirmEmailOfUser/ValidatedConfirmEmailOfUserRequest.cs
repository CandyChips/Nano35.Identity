using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Processor.UseCase.ConfirmEmailOfUser
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
            IPipeNode<IConfirmEmailOfUserRequestContract, IConfirmEmailOfUserResultContract> next) :
            base(next)
        { }

        public override Task<IConfirmEmailOfUserResultContract> Ask(
            IConfirmEmailOfUserRequestContract input,
            CancellationToken cancellationToken)
        {
            return DoNext(input, cancellationToken);
        }
    }
}