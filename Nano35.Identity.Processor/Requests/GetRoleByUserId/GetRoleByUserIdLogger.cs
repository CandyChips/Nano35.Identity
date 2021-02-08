using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Processor.Requests.GetRoleByUserId
{
    public class GetRoleByUserIdLogger :
        IPipelineNode<IGetRoleByUserIdRequestContract, IGetRoleByUserIdResultContract>
    {
        private readonly ILogger<GetRoleByUserIdLogger> _logger;
        private readonly IPipelineNode<IGetRoleByUserIdRequestContract, IGetRoleByUserIdResultContract> _nextNode;

        public GetRoleByUserIdLogger(
            ILogger<GetRoleByUserIdLogger> logger,
            IPipelineNode<IGetRoleByUserIdRequestContract, IGetRoleByUserIdResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetRoleByUserIdResultContract> Ask(IGetRoleByUserIdRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetRoleByUserIdLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetRoleByUserIdLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}