using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Nano35.Identity.Api.Services.Requests.Behaviours
{
    public class CacheQueryPipeLineBehaviour<TIn, TOut> :
        IPipelineBehavior<TIn, TOut>
        where TIn : IQueryRequest<TOut>
    {
        private readonly ILogger<CacheQueryPipeLineBehaviour<TIn, TOut>> _logger;

        public CacheQueryPipeLineBehaviour(
            ILogger<CacheQueryPipeLineBehaviour<TIn, TOut>> logger)
        {
            _logger = logger;
        }
        
        public async Task<TOut> Handle(
            TIn request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TOut> next)
        {
            _logger.LogInformation("Check cache");
            var response = await next();
            return response;
        }
    }
}