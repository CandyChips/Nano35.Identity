using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Nano35.Identity.Api.Services.Requests.Behaviours
{
    public class LoggingPipeLineBehaviour<TRequest, TResponse> 
                : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingPipeLineBehaviour<TRequest, TResponse>> _logger;
        public LoggingPipeLineBehaviour(
            ILogger<LoggingPipeLineBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request, 
            CancellationToken cancellationToken, 
            RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogInformation($"Request {typeof(TRequest).Name}");
            var response = await next().ConfigureAwait(false);
            _logger.LogInformation($"Response {typeof(TResponse).Name}:{response}");
            return response;
        }
    }
}