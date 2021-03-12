﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GenerateToken
{
    public class LoggedGenerateTokenRequest :
        PipeNodeBase<IGenerateTokenRequestContract, IGenerateTokenResultContract>
    {
        private readonly ILogger<LoggedGenerateTokenRequest> _logger;

        public LoggedGenerateTokenRequest(
            ILogger<LoggedGenerateTokenRequest> logger,
            IPipeNode<IGenerateTokenRequestContract, IGenerateTokenResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override Task<IGenerateTokenResultContract> Ask(IGenerateTokenRequestContract input)
        {
            _logger.LogInformation($"GenerateTokenLogger starts on: {DateTime.Now}");
            var result = DoNext(input);
            _logger.LogInformation($"GenerateTokenLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}