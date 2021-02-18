using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.CreateUser
{
    public class LoggedCreateUserRequest :
        IPipelineNode<ICreateUserRequestContract, ICreateUserResultContract>
    {
        private readonly ILogger<LoggedCreateUserRequest> _logger;
        private readonly IPipelineNode<ICreateUserRequestContract, ICreateUserResultContract> _nextNode;

        public LoggedCreateUserRequest(
            ILogger<LoggedCreateUserRequest> logger,
            IPipelineNode<ICreateUserRequestContract, ICreateUserResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<ICreateUserResultContract> Ask(
            ICreateUserRequestContract input)
        {
            _logger.LogInformation($"CreateUserLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"CreateUserLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}