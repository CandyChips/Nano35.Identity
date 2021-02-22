using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Processor.Requests.UpdateName
{
    public class LoggedUpdateNameRequest :
        IPipelineNode<
            IUpdateNameRequestContract,
            IUpdateNameResultContract>
    {
        private readonly ILogger<LoggedUpdateNameRequest> _logger;
        private readonly IPipelineNode<
            IUpdateNameRequestContract, 
            IUpdateNameResultContract> _nextNode;

        public LoggedUpdateNameRequest(
            ILogger<LoggedUpdateNameRequest> logger,
            IPipelineNode<
                IUpdateNameRequestContract, 
                IUpdateNameResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateNameResultContract> Ask(
            IUpdateNameRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"UpdateNameLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"UpdateNameLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}