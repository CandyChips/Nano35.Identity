using System.Threading;
using System.Threading.Tasks;
using LanguageExt;
using Nano35.Contracts;

namespace Nano35.Identity.Processor.UseCase
{
    public interface IRailPipeNode<in TIn, TOut>
        where TIn : IRequest
        where TOut : ISuccess
    {
        Task<Either<string, TOut>> Ask(TIn input, CancellationToken cancellationToken);
    }

    public abstract class RailPipeNodeBase<TIn, TOut> : 
        IRailPipeNode<TIn, TOut>
        where TIn : IRequest
        where TOut : ISuccess
    {
        private readonly IRailPipeNode<TIn, TOut> _next;
        protected RailPipeNodeBase(IRailPipeNode<TIn, TOut> next) { _next = next; }
        protected Task<Either<string, TOut>> DoNext(TIn input, CancellationToken cancellationToken) => _next.Ask(input, cancellationToken);
        public abstract Task<Either<string, TOut>> Ask(TIn input, CancellationToken cancellationToken);
    }

    public abstract class RailEndPointNodeBase<TIn, TOut> : 
        IRailPipeNode<TIn, TOut>
        where TIn : IRequest
        where TOut : ISuccess
    {
        public abstract Task<Either<string, TOut>> Ask(TIn input, CancellationToken cancellationToken);
    }
}