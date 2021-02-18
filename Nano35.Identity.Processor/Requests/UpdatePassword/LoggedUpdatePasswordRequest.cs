using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Processor.Requests.UpdatePassword
{
    public class LoggedGetUserByRoleIdRequest :
        IPipelineNode<IUpdatePasswordRequestContract, IUpdatePasswordResultContract>
    {
        private readonly ILogger<LoggedGetUserByRoleIdRequest> _logger;
        private readonly IPipelineNode<IUpdatePasswordRequestContract, IUpdatePasswordResultContract> _nextNode;

        public LoggedGetUserByRoleIdRequest(
            ILogger<LoggedGetUserByRoleIdRequest> logger,
            IPipelineNode<IUpdatePasswordRequestContract, IUpdatePasswordResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdatePasswordResultContract> Ask(IUpdatePasswordRequestContract input,
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