using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Identity.Api.Requests
{
    public class MasstransitRequest<TMessage, TResponse> 
        where TMessage : class, IRequest
        where TResponse : IResult
    {
        private readonly IRequestClient<TMessage> _requestClient;
        private readonly TMessage _request;

        public MasstransitRequest(IBus bus, TMessage request)
        {
            _requestClient = bus.CreateRequestClient<TMessage>(TimeSpan.FromSeconds(10));
            _request = request;
        }

        public async Task<UseCaseResponse<TResponse>> GetResponse()
        {
            var responseGetClientString = await _requestClient.GetResponse<UseCaseResponse<TResponse>>(_request);
            return responseGetClientString.Message;
        }
    }
}