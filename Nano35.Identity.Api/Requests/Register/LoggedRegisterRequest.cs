﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.Register
{
    public class LoggedRegisterRequest :
        PipeNodeBase<IRegisterRequestContract, IRegisterResultContract>
    {
        private readonly ILogger<LoggedRegisterRequest> _logger;
        
        public LoggedRegisterRequest(
            ILogger<LoggedRegisterRequest> logger,
            IPipeNode<IRegisterRequestContract, IRegisterResultContract> next) : 
            base (next)
        {
            _logger = logger;
        }

        public override Task<IRegisterResultContract> Ask(IRegisterRequestContract input)
        {
            _logger.LogInformation($"RegisterLogger starts on: {DateTime.Now}");
            var result = DoNext(input);
            _logger.LogInformation($"RegisterLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}