﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GetUserById
{
    public class GetUserByIdLogger :
        IPipelineNode<IGetUserByIdRequestContract, IGetUserByIdResultContract>
    {
        private readonly ILogger<GetUserByIdLogger> _logger;
        private readonly IPipelineNode<IGetUserByIdRequestContract, IGetUserByIdResultContract> _nextNode;

        public GetUserByIdLogger(
            ILogger<GetUserByIdLogger> logger,
            IPipelineNode<IGetUserByIdRequestContract, IGetUserByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetUserByIdResultContract> Ask(
            IGetUserByIdRequestContract input)
        {
            _logger.LogInformation($"GetUserByIdLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"GetUserByIdLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}