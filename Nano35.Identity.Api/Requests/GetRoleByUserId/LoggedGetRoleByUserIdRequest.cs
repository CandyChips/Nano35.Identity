using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GetRoleByUserId
{
    public class LoggedGetRoleByUserIdRequest :
        IPipelineNode<IGetRoleByUserIdRequestContract, IGetRoleByUserIdResultContract>
    {
        private readonly ILogger<LoggedGetRoleByUserIdRequest> _logger;
        private readonly IPipelineNode<IGetRoleByUserIdRequestContract, IGetRoleByUserIdResultContract> _nextNode;

        public LoggedGetRoleByUserIdRequest(
            ILogger<LoggedGetRoleByUserIdRequest> logger,
            IPipelineNode<IGetRoleByUserIdRequestContract, IGetRoleByUserIdResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetRoleByUserIdResultContract> Ask(
            IGetRoleByUserIdRequestContract input)
        {
            _logger.LogInformation($"GetRoleByUserIdLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"GetRoleByUserIdLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}