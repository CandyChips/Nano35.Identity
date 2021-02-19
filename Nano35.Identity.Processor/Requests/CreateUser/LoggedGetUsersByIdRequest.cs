using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Processor.Requests.CreateUser
{
    public class LoggedGetUserByRoleIdRequest :
        IPipelineNode<
            ICreateUserRequestContract,
            ICreateUserResultContract>
    {
        private readonly ILogger<LoggedGetUserByRoleIdRequest> _logger;
        
        private readonly IPipelineNode<
            ICreateUserRequestContract,
            ICreateUserResultContract> _nextNode;

        public LoggedGetUserByRoleIdRequest(
            ILogger<LoggedGetUserByRoleIdRequest> logger,
            IPipelineNode<
                ICreateUserRequestContract,
                ICreateUserResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<ICreateUserResultContract> Ask(
            ICreateUserRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetUserByRoleIdLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetUserByRoleIdLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}