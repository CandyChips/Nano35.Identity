using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Processor.UseCase.Register
{
    public class ValidatedRegisterRequestErrorResult :
        IRegisterErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedRegisterRequest:
        PipeNodeBase<IRegisterRequestContract, IRegisterResultContract>
    {
        public ValidatedRegisterRequest(
            IPipeNode<IRegisterRequestContract, IRegisterResultContract> next) :
            base(next) {}

        public override Task<IRegisterResultContract> Ask(IRegisterRequestContract input,
            CancellationToken cancellationToken)
        {
            return DoNext(input, cancellationToken);
        }
    }
}