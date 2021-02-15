﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GetAllRoles
{
    public class LoggedGetAllRolesRequest :
        IPipelineNode<IGetAllRolesRequestContract, IGetAllRolesResultContract>
    {
        private readonly ILogger<LoggedGetAllRolesRequest> _logger;
        private readonly IPipelineNode<IGetAllRolesRequestContract, IGetAllRolesResultContract> _nextNode;

        public LoggedGetAllRolesRequest(
            ILogger<LoggedGetAllRolesRequest> logger,
            IPipelineNode<IGetAllRolesRequestContract, IGetAllRolesResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllRolesResultContract> Ask(
            IGetAllRolesRequestContract input)
        {
            _logger.LogInformation($"GetAllRolesLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"GetAllRolesLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}