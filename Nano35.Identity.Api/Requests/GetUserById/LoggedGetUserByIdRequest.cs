﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GetUserById
{
    public class LoggedGetUserByIdRequest :
        PipeNodeBase<IGetUserByIdRequestContract, IGetUserByIdResultContract>
    {
        private readonly ILogger<LoggedGetUserByIdRequest> _logger;
        
        public LoggedGetUserByIdRequest(
            ILogger<LoggedGetUserByIdRequest> logger,
            IPipeNode<IGetUserByIdRequestContract, IGetUserByIdResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override Task<IGetUserByIdResultContract> Ask(IGetUserByIdRequestContract input)
        {
            _logger.LogInformation($"GetUserByIdLogger starts on: {DateTime.Now}");
            var result = DoNext(input);
            _logger.LogInformation($"GetUserByIdLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}