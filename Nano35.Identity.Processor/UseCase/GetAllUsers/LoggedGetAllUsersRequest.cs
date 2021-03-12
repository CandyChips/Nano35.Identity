using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Processor.UseCase.GetAllUsers
{
    public class LoggedGetAllUsersRequest :
        PipeNodeBase<IGetAllUsersRequestContract, IGetAllUsersResultContract>
    {
        private readonly ILogger<LoggedGetAllUsersRequest> _logger;
        
        public LoggedGetAllUsersRequest(
            ILogger<LoggedGetAllUsersRequest> logger,
            IPipeNode<IGetAllUsersRequestContract, IGetAllUsersResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override Task<IGetAllUsersResultContract> Ask(
            IGetAllUsersRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllUsersLogger starts on: {DateTime.Now}");
            var result = DoNext(input, cancellationToken);
            _logger.LogInformation($"GetAllUsersLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}