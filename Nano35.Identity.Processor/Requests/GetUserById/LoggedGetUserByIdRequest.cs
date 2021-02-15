using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Processor.Requests.GetUserById
{
    public class LoggedGetUserByIdRequest :
        IPipelineNode<IGetUserByIdRequestContract, IGetUserByIdResultContract>
    {
        private readonly ILogger<LoggedGetUserByIdRequest> _logger;
        private readonly IPipelineNode<IGetUserByIdRequestContract, IGetUserByIdResultContract> _nextNode;

        public LoggedGetUserByIdRequest(
            ILogger<LoggedGetUserByIdRequest> logger,
            IPipelineNode<IGetUserByIdRequestContract, IGetUserByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetUserByIdResultContract> Ask(IGetUserByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetUserByIdLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetUserByIdLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}