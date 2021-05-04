using System.Threading.Tasks;
using Nano35.Contracts;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Identity.Api.Requests
{
    public interface IPipeNode<in TIn, TOut>
        where TOut : IResult
    {
        Task<UseCaseResponse<TOut>> Ask(TIn input);
    }
    
    public abstract class PipeNodeBase<TIn, TOut> : IPipeNode<TIn, TOut>
        where TIn : IRequest
        where TOut : IResult
    {
        private readonly IPipeNode<TIn, TOut> _next;
        protected PipeNodeBase(IPipeNode<TIn, TOut> next) => _next = next;
        protected Task<UseCaseResponse<TOut>> DoNext(TIn input) => _next.Ask(input);
        public abstract Task<UseCaseResponse<TOut>> Ask(TIn input);
    }

    public abstract class EndPointNodeBase<TIn, TOut> : IPipeNode<TIn, TOut>
        where TIn : IRequest
        where TOut : IResult
    {
        public abstract Task<UseCaseResponse<TOut>> Ask(TIn input);
    }
}