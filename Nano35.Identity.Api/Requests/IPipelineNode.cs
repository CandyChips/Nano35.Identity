using System.Threading.Tasks;

namespace Nano35.Identity.Api.Requests
{
    public interface IPipelineNode<TIn, TOut>
    {
        Task<TOut> Ask(TIn input);
    }
}