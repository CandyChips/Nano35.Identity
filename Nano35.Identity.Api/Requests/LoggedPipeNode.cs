using System;
using System.Threading;
using System.Threading.Tasks;
using LanguageExt;
using Microsoft.Extensions.Logging;
using Nano35.Contracts;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Identity.Api.Requests
{
    public class LoggedUseCasePipeNode<TIn, TOut> : UseCasePipeNodeBase<TIn, TOut>
        where TIn : IRequest
        where TOut : IResult
    {
        private readonly ILogger<TIn> _logger;
        public LoggedUseCasePipeNode(ILogger<TIn> logger, IUseCasePipeNode<TIn, TOut> next) : base(next) => _logger = logger;
        public override async Task<UseCaseResponse<TOut>> Ask(TIn input)
        {
            try
            {
                var starts = DateTime.Now;
                var result = await DoNext(input);
                var time = DateTime.Now - starts;
                _logger.LogInformation(result.IsSuccess()
                    ? $"{typeof(TIn)} ends by: {time} with success."
                    : $"{typeof(TIn)} ends by: {time} with error: {result.Error}.");
                return result;
            }
            catch (Exception e)
            {
                _logger.LogInformation($"{typeof(TIn)} ends by: {DateTime.Now} with exception!!!");
                return new UseCaseResponse<TOut>($"{typeof(TIn)} ends by: {DateTime.Now} with exception!!!");
            }
        }
    }
}