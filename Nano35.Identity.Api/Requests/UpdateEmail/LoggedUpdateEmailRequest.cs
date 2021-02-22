using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.UpdateEmail
{
    public class LoggedUpdateEmailRequest :
        IPipelineNode<
            IUpdateEmailRequestContract,
            IUpdateEmailResultContract>
    {
        private readonly ILogger<LoggedUpdateEmailRequest> _logger;
        
        private readonly IPipelineNode<
            IUpdateEmailRequestContract, 
            IUpdateEmailResultContract> _nextNode;

        public LoggedUpdateEmailRequest(
            ILogger<LoggedUpdateEmailRequest> logger,
            IPipelineNode<
                IUpdateEmailRequestContract, 
                IUpdateEmailResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateEmailResultContract> Ask(
            IUpdateEmailRequestContract input)
        {
            _logger.LogInformation($"UpdateEmailLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"UpdateEmailLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}