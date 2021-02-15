using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Processor.Requests.GetAllUsers
{
    public class LoggedGetAllUsersRequest :
        IPipelineNode<IGetAllUsersRequestContract, IGetAllUsersResultContract>
    {
        private readonly ILogger<LoggedGetAllUsersRequest> _logger;
        private readonly IPipelineNode<IGetAllUsersRequestContract, IGetAllUsersResultContract> _nextNode;

        public LoggedGetAllUsersRequest(
            ILogger<LoggedGetAllUsersRequest> logger,
            IPipelineNode<IGetAllUsersRequestContract, IGetAllUsersResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllUsersResultContract> Ask(IGetAllUsersRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllUsersLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetAllUsersLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}