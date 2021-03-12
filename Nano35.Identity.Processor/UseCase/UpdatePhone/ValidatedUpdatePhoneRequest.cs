using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Processor.UseCase.UpdatePhone
{
    public class ValidatedUpdatePhoneRequestErrorResult : 
        IUpdatePhoneErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdatePhoneRequest:
        PipeNodeBase<IUpdatePhoneRequestContract, IUpdatePhoneResultContract>
    {
        public ValidatedUpdatePhoneRequest(
            IPipeNode<IUpdatePhoneRequestContract, IUpdatePhoneResultContract> next) :
            base(next) {}

        public override Task<IUpdatePhoneResultContract> Ask(
            IUpdatePhoneRequestContract input,
            CancellationToken cancellationToken)
        {
            return DoNext(input, cancellationToken);
        }
    }
}