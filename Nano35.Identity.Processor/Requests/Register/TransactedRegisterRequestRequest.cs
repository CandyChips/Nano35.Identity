using System;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.Requests.Register
{
    public class TransactedRegisterRequest :
        IPipelineNode<
            IRegisterRequestContract,
            IRegisterResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IPipelineNode<
            IRegisterRequestContract,
            IRegisterResultContract> _nextNode;

        public TransactedRegisterRequest(
            ApplicationContext context,
            IPipelineNode<
                IRegisterRequestContract,
                IRegisterResultContract> nextNode)
        {
            _nextNode = nextNode;
            _context = context;
        }

        public async Task<IRegisterResultContract> Ask(
            IRegisterRequestContract input,
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