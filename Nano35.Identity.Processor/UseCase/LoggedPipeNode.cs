using System;
using System.Threading;
using System.Threading.Tasks;
using LanguageExt;
using Microsoft.Extensions.Logging;
using Nano35.Contracts;

namespace Nano35.Identity.Processor.UseCase
{
    public class LoggedRailPipeNode<TIn, TOut> : 
        RailPipeNodeBase<TIn, TOut>
        where TIn : IRequest
        where TOut : ISuccess
    {
        private readonly ILogger<TIn> _logger;

        public LoggedRailPipeNode(
            ILogger<TIn> logger,
            IRailPipeNode<TIn, TOut> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<Either<string, TOut>> Ask(TIn input, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{typeof(TIn)} starts on: {DateTime.Now}.");
            var result = await DoNext(input, cancellationToken);
            result.Match(
                r => 
                    _logger.LogInformation($"{typeof(TIn)} ends on: {DateTime.Now} with success."),
                e => 
                    _logger.LogInformation($"{typeof(TIn)} ends on: {DateTime.Now} with error {e}."));
            return result;
        }
    }
}