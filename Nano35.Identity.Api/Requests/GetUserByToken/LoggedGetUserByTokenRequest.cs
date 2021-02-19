using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GetUserByToken
{
    public class LoggedGetUserByTokenRequest :
        IPipelineNode<
            IGetUserByIdRequestContract,
            IGetUserByIdResultContract>
    {
        private readonly ILogger<LoggedGetUserByTokenRequest> _logger;
        
        private readonly IPipelineNode<
            IGetUserByIdRequestContract,
            IGetUserByIdResultContract> _nextNode;

        public LoggedGetUserByTokenRequest(
            ILogger<LoggedGetUserByTokenRequest> logger,
            IPipelineNode<
                IGetUserByIdRequestContract,
                IGetUserByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetUserByIdResultContract> Ask(
            IGetUserByIdRequestContract input)
        {
            _logger.LogInformation($"GetUserByTokenLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"GetUserByTokenLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}