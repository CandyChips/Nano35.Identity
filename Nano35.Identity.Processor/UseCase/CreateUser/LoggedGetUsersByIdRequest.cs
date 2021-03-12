using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Processor.UseCase.CreateUser
{
    public class LoggedGetUserByRoleIdRequest :
        PipeNodeBase<ICreateUserRequestContract, ICreateUserResultContract>
    {
        private readonly ILogger<LoggedGetUserByRoleIdRequest> _logger;
        
        public LoggedGetUserByRoleIdRequest(
            ILogger<LoggedGetUserByRoleIdRequest> logger,
            IPipeNode<ICreateUserRequestContract, ICreateUserResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override Task<ICreateUserResultContract> Ask(
            ICreateUserRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetUserByRoleIdLogger starts on: {DateTime.Now}");
            var result = DoNext(input, cancellationToken);
            _logger.LogInformation($"GetUserByRoleIdLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}