using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Processor.UseCase.GenerateToken
{
    public class ValidatedGenerateTokenRequest:
        PipeNodeBase<IGenerateTokenRequestContract, IGenerateTokenResultContract>
    {
        public ValidatedGenerateTokenRequest(
            IPipeNode<IGenerateTokenRequestContract, IGenerateTokenResultContract> next) :
            base(next) {}

        public override Task<IGenerateTokenResultContract> Ask(
            IGenerateTokenRequestContract input,
            CancellationToken cancellationToken)
        {
            return DoNext(input, cancellationToken);
        }
    }
}