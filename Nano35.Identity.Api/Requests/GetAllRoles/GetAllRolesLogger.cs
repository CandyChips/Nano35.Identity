﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GetAllRoles
{
    public class GetAllRolesLogger :
        IPipelineNode<IGetAllRolesRequestContract, IGetAllRolesResultContract>
    {
        private readonly ILogger<GetAllRolesLogger> _logger;
        private readonly IPipelineNode<IGetAllRolesRequestContract, IGetAllRolesResultContract> _nextNode;

        public GetAllRolesLogger(
            ILogger<GetAllRolesLogger> logger,
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