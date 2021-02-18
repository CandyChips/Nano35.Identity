using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Processor.Requests.UpdatePhone
{
    public class LoggedGetUserByRoleIdRequest :
        IPipelineNode<IUpdatePhoneRequestContract, IUpdatePhoneResultContract>
    {
        private readonly ILogger<LoggedGetUserByRoleIdRequest> _logger;
        private readonly IPipelineNode<IUpdatePhoneRequestContract, IUpdatePhoneResultContract> _nextNode;

        public LoggedGetUserByRoleIdRequest(
            ILogger<LoggedGetUserByRoleIdRequest> logger,
            IPipelineNode<IUpdatePhoneRequestContract, IUpdatePhoneResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdatePhoneResultContract> Ask(IUpdatePhoneRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetUserByRoleIdLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetUserByRoleIdLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}