using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.ConfirmEmailOfUser
{
    public class LoggedConfirmEmailOfUserRequest :
        IPipelineNode<
            IConfirmEmailOfUserRequestContract, 
            IConfirmEmailOfUserResultContract>
    {
        private readonly ILogger<LoggedConfirmEmailOfUserRequest> _logger;
        
        private readonly IPipelineNode<
            IConfirmEmailOfUserRequestContract,
            IConfirmEmailOfUserResultContract> _nextNode;

        public LoggedConfirmEmailOfUserRequest(
            ILogger<LoggedConfirmEmailOfUserRequest> logger,
            IPipelineNode<
                IConfirmEmailOfUserRequestContract,
                IConfirmEmailOfUserResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IConfirmEmailOfUserResultContract> Ask(
            IConfirmEmailOfUserRequestContract input)
        {
            _logger.LogInformation($"ConfirmEmailOfUserLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"ConfirmEmailOfUserLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}