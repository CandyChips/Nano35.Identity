using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.Requests.Behaviours
{
    public class TransactionPipeLineBehaviour<TIn, TOut> :
        IPipelineBehavior<TIn, TOut>
        where TIn : ICommandRequest<TOut>
    {
        private readonly ILogger<TransactionPipeLineBehaviour<TIn, TOut>> _logger;
        private readonly ApplicationContext _context;

        public TransactionPipeLineBehaviour(
            ILogger<TransactionPipeLineBehaviour<TIn, TOut>> logger, 
            ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }
        
        public async Task<TOut> Handle(TIn request, CancellationToken cancellationToken, RequestHandlerDelegate<TOut> next)
        {
            
            await using var transaction = _context.Database.BeginTransaction();
            var response = await next();
            if (response is IError)
            {
                await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                _logger.LogError("Transaction refused");
            }
            if (response is ISuccess)
            {
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            return response;
        }
    }
}