﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.UpdateEmail
{
    public class LoggedUpdateEmailRequest :
        PipeNodeBase<IUpdateEmailRequestContract, IUpdateEmailResultContract>
    {
        private readonly ILogger<LoggedUpdateEmailRequest> _logger;
        
        public LoggedUpdateEmailRequest(
            ILogger<LoggedUpdateEmailRequest> logger,
            IPipeNode<IUpdateEmailRequestContract, IUpdateEmailResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override Task<IUpdateEmailResultContract> Ask(IUpdateEmailRequestContract input)
        {
            _logger.LogInformation($"UpdateEmailLogger starts on: {DateTime.Now}");
            var result = DoNext(input);
            _logger.LogInformation($"UpdateEmailLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}