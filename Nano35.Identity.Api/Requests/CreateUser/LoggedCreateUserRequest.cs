using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.CreateUser
{
    public class LoggedCreateUserRequest :
        PipeNodeBase<ICreateUserRequestContract, ICreateUserResultContract>
    {
        private readonly ILogger<LoggedCreateUserRequest> _logger;

        public LoggedCreateUserRequest(
            ILogger<LoggedCreateUserRequest> logger,
            IPipeNode<ICreateUserRequestContract,ICreateUserResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override Task<ICreateUserResultContract> Ask(ICreateUserRequestContract input)
        {
            _logger.LogInformation($"CreateUserLogger starts on: {DateTime.Now}");
            var result = DoNext(input);
            _logger.LogInformation($"CreateUserLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}