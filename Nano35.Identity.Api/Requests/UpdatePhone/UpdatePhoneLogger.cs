using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.UpdatePhone
{
    public class UpdatePhoneLogger :
        IPipelineNode<IUpdatePhoneRequestContract, IUpdatePhoneResultContract>
    {
        private readonly ILogger<UpdatePhoneLogger> _logger;
        private readonly IPipelineNode<IUpdatePhoneRequestContract, IUpdatePhoneResultContract> _nextNode;

        public UpdatePhoneLogger(
            ILogger<UpdatePhoneLogger> logger,
            IPipelineNode<IUpdatePhoneRequestContract, IUpdatePhoneResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdatePhoneResultContract> Ask(
            IUpdatePhoneRequestContract input)
        {
            _logger.LogInformation($"UpdatePhoneLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"UpdatePhoneLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}