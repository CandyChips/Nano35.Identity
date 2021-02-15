using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Processor.Requests.GetRoleById
{
    public class LoggedGetRoleByIdRequest :
        IPipelineNode<IGetRoleByIdRequestContract, IGetRoleByIdResultContract>
    {
        private readonly ILogger<LoggedGetRoleByIdRequest> _logger;
        private readonly IPipelineNode<IGetRoleByIdRequestContract, IGetRoleByIdResultContract> _nextNode;

        public LoggedGetRoleByIdRequest(
            ILogger<LoggedGetRoleByIdRequest> logger,
            IPipelineNode<IGetRoleByIdRequestContract, IGetRoleByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetRoleByIdResultContract> Ask(IGetRoleByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetRoleByIdLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetRoleByIdLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}