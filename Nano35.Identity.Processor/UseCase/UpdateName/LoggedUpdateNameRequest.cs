using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Processor.UseCase.UpdateName
{
    public class LoggedUpdateNameRequest :
        PipeNodeBase<IUpdateNameRequestContract, IUpdateNameResultContract>
    {
        private readonly ILogger<LoggedUpdateNameRequest> _logger;

        public LoggedUpdateNameRequest(
            ILogger<LoggedUpdateNameRequest> logger,
            IPipeNode<IUpdateNameRequestContract, IUpdateNameResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override Task<IUpdateNameResultContract> Ask(
            IUpdateNameRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"UpdateNameLogger starts on: {DateTime.Now}");
            var result = DoNext(input, cancellationToken);
            _logger.LogInformation($"UpdateNameLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}