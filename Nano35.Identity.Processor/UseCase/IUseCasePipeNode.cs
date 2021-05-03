using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Identity.Processor.UseCase
{
    public interface IUseCasePipeNode<in TIn, TOut>
        where TIn : IRequest
        where TOut : IResult
    {
        Task<UseCaseResponse<TOut>> Ask(TIn input, CancellationToken cancellationToken);
    }
    
    public abstract class UseCasePipeNodeBase<TIn, TOut> : 
        IUseCasePipeNode<TIn, TOut>
        where TIn : IRequest
        where TOut : IResult
    {
        private readonly IUseCasePipeNode<TIn, TOut> _next;
        protected UseCasePipeNodeBase(IUseCasePipeNode<TIn, TOut> next) => _next = next;
        protected Task<UseCaseResponse<TOut>> DoNext(TIn input, CancellationToken cancellationToken) => _next.Ask(input, cancellationToken);
        public abstract Task<UseCaseResponse<TOut>> Ask(TIn input, CancellationToken cancellationToken);
    }

    public abstract class UseCaseEndPointNodeBase<TIn, TOut> : 
        IUseCasePipeNode<TIn, TOut>
        where TIn : IRequest
        where TOut : IResult
    {
        public abstract Task<UseCaseResponse<TOut>> Ask(TIn input, CancellationToken cancellationToken);
    }
    
    public class MasstransitUseCaseRequest<TIn, TOut> 
        where TIn : class, IRequest
        where TOut : IResult
    {
        private readonly IRequestClient<TIn> _requestClient;
        private readonly TIn _request;

        public MasstransitUseCaseRequest(IBus bus, TIn request)
        {
            _requestClient = bus.CreateRequestClient<TIn>(TimeSpan.FromSeconds(10));
            _request = request;
        }

        public async Task<UseCaseResponse<TOut>> GetResponse()
        {
            var responseGetClientString = await _requestClient.GetResponse<UseCaseResponse<TOut>>(_request);
            return responseGetClientString.Message;
        }
    }
}