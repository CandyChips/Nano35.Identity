using System.Threading.Tasks;
using Nano35.Contracts;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Identity.Api.Requests
{
    public interface IUseCasePipeNode<in TIn, TOut>
        where TOut : IResult
    {
        Task<UseCaseResponse<TOut>> Ask(TIn input);
    }
    
    public abstract class UseCasePipeNodeBase<TIn, TOut> : IUseCasePipeNode<TIn, TOut>
        where TIn : IRequest
        where TOut : IResult
    {
        private readonly IUseCasePipeNode<TIn, TOut> _next;
        protected UseCasePipeNodeBase(IUseCasePipeNode<TIn, TOut> next) => _next = next;
        protected Task<UseCaseResponse<TOut>> DoNext(TIn input) => _next.Ask(input);
        public abstract Task<UseCaseResponse<TOut>> Ask(TIn input);
    }

    public abstract class UseCaseEndPointNodeBase<TIn, TOut> : IUseCasePipeNode<TIn, TOut>
        where TIn : IRequest
        where TOut : IResult
    {
        public abstract Task<UseCaseResponse<TOut>> Ask(TIn input);
    }
}