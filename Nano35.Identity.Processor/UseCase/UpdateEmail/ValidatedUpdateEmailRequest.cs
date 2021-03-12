using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Processor.UseCase.UpdateEmail
{
    public class ValidatedUpdateEmailRequestErrorResult : 
        IUpdateEmailErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateEmailRequest:
        PipeNodeBase<IUpdateEmailRequestContract, IUpdateEmailResultContract>
    {
        public ValidatedUpdateEmailRequest(
            IPipeNode<IUpdateEmailRequestContract, IUpdateEmailResultContract> next) :
            base(next) {}

        public override Task<IUpdateEmailResultContract> Ask(
            IUpdateEmailRequestContract input,
            CancellationToken cancellationToken)
        {
            return DoNext(input, cancellationToken);
        }
    }
}