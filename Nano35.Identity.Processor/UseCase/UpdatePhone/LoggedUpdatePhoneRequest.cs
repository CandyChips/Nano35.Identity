﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Processor.UseCase.UpdatePhone
{
    public class LoggedUpdatePhoneRequest :
        PipeNodeBase<IUpdatePhoneRequestContract, IUpdatePhoneResultContract>
    {
        private readonly ILogger<LoggedUpdatePhoneRequest> _logger;

        public LoggedUpdatePhoneRequest(
            ILogger<LoggedUpdatePhoneRequest> logger,
            IPipeNode<IUpdatePhoneRequestContract, IUpdatePhoneResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override Task<IUpdatePhoneResultContract> Ask(
            IUpdatePhoneRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"UpdatePhoneLogger starts on: {DateTime.Now}");
            var result = DoNext(input, cancellationToken);
            _logger.LogInformation($"UpdatePhoneLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}