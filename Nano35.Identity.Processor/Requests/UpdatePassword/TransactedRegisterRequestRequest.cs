using System;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.Requests.UpdatePassword
{
    public class TransactedUpdatePasswordRequest :
        IPipelineNode<
            IUpdatePasswordRequestContract,
            IUpdatePasswordResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IPipelineNode<
            IUpdatePasswordRequestContract,
            IUpdatePasswordResultContract> _nextNode;

        public TransactedUpdatePasswordRequest(
            ApplicationContext context,
            IPipelineNode<
                IUpdatePasswordRequestContract,
                IUpdatePasswordResultContract> nextNode)
        {
            _nextNode = nextNode;
            _context = context;
        }

        public async Task<IUpdatePasswordResultContract> Ask(
            IUpdatePasswordRequestContract input,
            CancellationToken cancellationToken)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var response = await _nextNode.Ask(input, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return response;
            }
            catch(Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                throw new Exception("Транзакция отменена", ex);
            }
        }
    }
}