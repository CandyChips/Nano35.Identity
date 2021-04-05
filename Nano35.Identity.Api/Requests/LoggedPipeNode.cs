using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts;

namespace Nano35.Identity.Api.Requests
{
    public class LoggedPipeNode<TIn, TOut> : PipeNodeBase<TIn, TOut>
        where TIn : IRequest
        where TOut : IResponse
    {
        private readonly ILogger<TIn> _logger;

        public LoggedPipeNode(
            ILogger<TIn> logger,
            IPipeNode<TIn, TOut> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<TOut> Ask(TIn input)
        {
            _logger.LogInformation($"{typeof(TIn)} starts on: {DateTime.Now}.");
            var result = await DoNext(input);
            _logger.LogInformation($"{typeof(TIn)} ends on: {DateTime.Now}.");
            return result;
        }
    }
}