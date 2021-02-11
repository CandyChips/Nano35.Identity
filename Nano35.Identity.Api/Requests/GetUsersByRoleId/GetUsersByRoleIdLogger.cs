﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GetUsersByRoleId
{
    public class GetUsersByRoleIdLogger :
        IPipelineNode<IGetUsersByRoleIdRequestContract, IGetUsersByRoleIdResultContract>
    {
        private readonly ILogger<GetUsersByRoleIdLogger> _logger;
        private readonly IPipelineNode<IGetUsersByRoleIdRequestContract, IGetUsersByRoleIdResultContract> _nextNode;

        public GetUsersByRoleIdLogger(
            ILogger<GetUsersByRoleIdLogger> logger,
            IPipelineNode<IGetUsersByRoleIdRequestContract, IGetUsersByRoleIdResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetUsersByRoleIdResultContract> Ask(
            IGetUsersByRoleIdRequestContract input)
        {
            _logger.LogInformation($"GetUsersByRoleIdLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"GetUsersByRoleIdLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}