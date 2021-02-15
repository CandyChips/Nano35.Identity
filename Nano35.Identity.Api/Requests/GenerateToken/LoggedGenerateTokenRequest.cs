﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GenerateToken
{
    public class LoggedGenerateTokenRequest :
        IPipelineNode<IGenerateTokenRequestContract, IGenerateTokenResultContract>
    {
        private readonly ILogger<LoggedGenerateTokenRequest> _logger;
        private readonly IPipelineNode<IGenerateTokenRequestContract, IGenerateTokenResultContract> _nextNode;

        public LoggedGenerateTokenRequest(
            ILogger<LoggedGenerateTokenRequest> logger,
            IPipelineNode<IGenerateTokenRequestContract, IGenerateTokenResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGenerateTokenResultContract> Ask(
            IGenerateTokenRequestContract input)
        {
            _logger.LogInformation($"GenerateTokenLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"GenerateTokenLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}