using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Nano35.Identity.Api.Requests.Behaviours
{
    public class LoggerQueryDecorator<TIn, TOut> :
        IPipelineBehavior<TIn, TOut>
        where TIn : IQueryRequest<TOut>
    {
        private readonly ILogger<LoggerQueryDecorator<TIn, TOut>> _logger;

        public LoggerQueryDecorator(
            ILogger<LoggerQueryDecorator<TIn, TOut>> logger)
        {
            _logger = logger;
        }

        public async Task<TOut> Handle(
            TIn request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TOut> next)
        {
            _logger.LogInformation($"Before Query - {DateTime.Now}");
            var response = await next();
            _logger.LogInformation($"After Query - {DateTime.Now}");
            return response;
        }
    }
}