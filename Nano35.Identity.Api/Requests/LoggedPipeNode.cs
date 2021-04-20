using System;
using System.Threading;
using System.Threading.Tasks;
using LanguageExt;
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
            var starts = DateTime.Now;
            var result = await DoNext(input);
            switch (result)
            {
                case ISuccess:
                    _logger.LogInformation($"{typeof(TIn)} ends by: {starts - DateTime.Now} with success.");
                    break;
                case IError error:
                    _logger.LogInformation($"{typeof(TIn)} ends by: {starts - DateTime.Now} with error: {error}.");
                    break;
                default:
                    _logger.LogInformation($"{typeof(TIn)} ends by: {starts - DateTime.Now} with strange error!!!");
                    break;
            }
            return result;
        }
    }
    
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

        public override async Task<Either<string, TOut>> Ask(TIn input)
        {
            _logger.LogInformation($"{typeof(TIn)} starts on: {DateTime.Now}.");
            var result = await DoNext(input);
            result.Match(
                Right: r => 
                    _logger.LogInformation($"{typeof(TIn)} ends on: {DateTime.Now} with success."),
                Left: e => 
                    _logger.LogInformation($"{typeof(TIn)} ends on: {DateTime.Now} with error {e}."));
            return result;
        }
    }
}