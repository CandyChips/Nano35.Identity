using System.Threading;
using System.Threading.Tasks;

namespace Nano35.Identity.Processor.Requests
{
    public interface IPipelineNode<TIn, TOut>
    {
        Task<TOut> Ask(TIn input, CancellationToken cancellationToken);
    }
}