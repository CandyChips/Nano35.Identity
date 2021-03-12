using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Processor.UseCase.UpdatePassword
{
    public class LoggedUpdatePasswordRequest :
        PipeNodeBase<IUpdatePasswordRequestContract, IUpdatePasswordResultContract>
    {
        private readonly ILogger<LoggedUpdatePasswordRequest> _logger;
        
        public LoggedUpdatePasswordRequest(
            ILogger<LoggedUpdatePasswordRequest> logger,
            IPipeNode<IUpdatePasswordRequestContract, IUpdatePasswordResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override Task<IUpdatePasswordResultContract> Ask(
            IUpdatePasswordRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"UpdatePasswordLogger starts on: {DateTime.Now}");
            var result = DoNext(input, cancellationToken);
            _logger.LogInformation($"UpdatePasswordLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}