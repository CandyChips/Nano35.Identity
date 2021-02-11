using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.Register
{
    public class RegisterLogger :
        IPipelineNode<IRegisterRequestContract, IRegisterResultContract>
    {
        private readonly ILogger<RegisterLogger> _logger;
        private readonly IPipelineNode<IRegisterRequestContract, IRegisterResultContract> _nextNode;

        public RegisterLogger(
            ILogger<RegisterLogger> logger,
            IPipelineNode<IRegisterRequestContract, IRegisterResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IRegisterResultContract> Ask(
            IRegisterRequestContract input)
        {
            _logger.LogInformation($"RegisterLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"RegisterLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}