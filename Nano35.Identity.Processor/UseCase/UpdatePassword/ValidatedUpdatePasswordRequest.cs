using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Processor.UseCase.UpdatePassword
{
    public class ValidatedUpdatePasswordRequestErrorResult : 
        IUpdatePasswordErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdatePasswordRequest:
        PipeNodeBase<IUpdatePasswordRequestContract, IUpdatePasswordResultContract>
    {
        public ValidatedUpdatePasswordRequest(
            IPipeNode<IUpdatePasswordRequestContract, IUpdatePasswordResultContract> next) :
            base(next) {}

        public override Task<IUpdatePasswordResultContract> Ask(
            IUpdatePasswordRequestContract input,
            CancellationToken cancellationToken)
        {
            return DoNext(input, cancellationToken);
        }
    }
}