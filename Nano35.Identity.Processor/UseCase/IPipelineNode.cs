using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts;

namespace Nano35.Identity.Processor.UseCase
{
    public interface IPipeNode<in TIn, TOut>
    {
        Task<TOut> Ask(TIn input, CancellationToken cancellationToken);
    }

    public abstract class PipeNodeBase<TIn, TOut> : 
        IPipeNode<TIn, TOut>
        where TIn : IRequest
        where TOut : IResponse
    {
        private readonly IPipeNode<TIn, TOut> _next;
        protected PipeNodeBase(IPipeNode<TIn, TOut> next) { _next = next; }
        protected Task<TOut> DoNext(TIn input, CancellationToken cancellationToken) { return _next.Ask(input, cancellationToken); }
        public abstract Task<TOut> Ask(TIn input, CancellationToken cancellationToken);
    }

    public abstract class EndPointNodeBase<TIn, TOut> : 
        IPipeNode<TIn, TOut>
        where TIn : IRequest
        where TOut : IResponse
    {
        public abstract Task<TOut> Ask(TIn input, CancellationToken cancellationToken);
    }
}