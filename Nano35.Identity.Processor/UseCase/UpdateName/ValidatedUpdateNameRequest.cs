using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Processor.UseCase.UpdateName
{
    public class ValidatedUpdateNameRequest:
        PipeNodeBase<IUpdateNameRequestContract, IUpdateNameResultContract>
    {
        public ValidatedUpdateNameRequest(
            IPipeNode<IUpdateNameRequestContract, IUpdateNameResultContract> next) :
            base(next) {}

        public override Task<IUpdateNameResultContract> Ask(
            IUpdateNameRequestContract input,
            CancellationToken cancellationToken)
        {
            return DoNext(input, cancellationToken);
        }
    }
}