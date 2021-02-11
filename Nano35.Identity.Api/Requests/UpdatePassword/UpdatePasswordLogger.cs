using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.UpdatePassword
{
    public class UpdatePasswordLogger :
        IPipelineNode<IUpdatePasswordRequestContract, IUpdatePasswordResultContract>
    {
        private readonly ILogger<UpdatePasswordLogger> _logger;
        private readonly IPipelineNode<IUpdatePasswordRequestContract, IUpdatePasswordResultContract> _nextNode;

        public UpdatePasswordLogger(
            ILogger<UpdatePasswordLogger> logger,
            IPipelineNode<IUpdatePasswordRequestContract, IUpdatePasswordResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdatePasswordResultContract> Ask(
            IUpdatePasswordRequestContract input)
        {
            _logger.LogInformation($"UpdatePasswordLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"UpdatePasswordLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}