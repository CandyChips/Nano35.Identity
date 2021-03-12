using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GetUserByToken
{
    public class LoggedGetUserByTokenRequest :
        PipeNodeBase<IGetUserByIdRequestContract, IGetUserByIdResultContract>
    {
        private readonly ILogger<LoggedGetUserByTokenRequest> _logger;

        public LoggedGetUserByTokenRequest(
            ILogger<LoggedGetUserByTokenRequest> logger,
            IPipeNode<IGetUserByIdRequestContract, IGetUserByIdResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override Task<IGetUserByIdResultContract> Ask(IGetUserByIdRequestContract input)
        {
            _logger.LogInformation($"GetUserByTokenLogger starts on: {DateTime.Now}");
            var result = DoNext(input);
            _logger.LogInformation($"GetUserByTokenLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}