﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.ConfirmEmailOfUser
{
    public class LoggedConfirmEmailOfUserRequest :
        PipeNodeBase<IConfirmEmailOfUserRequestContract, IConfirmEmailOfUserResultContract>
    {
        private readonly ILogger<LoggedConfirmEmailOfUserRequest> _logger;
        
        public LoggedConfirmEmailOfUserRequest(
            ILogger<LoggedConfirmEmailOfUserRequest> logger,
            IPipeNode<IConfirmEmailOfUserRequestContract, IConfirmEmailOfUserResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override Task<IConfirmEmailOfUserResultContract> Ask(IConfirmEmailOfUserRequestContract input)
        {
            _logger.LogInformation($"ConfirmEmailOfUserLogger starts on: {DateTime.Now}");
            var result = DoNext(input);
            _logger.LogInformation($"ConfirmEmailOfUserLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}