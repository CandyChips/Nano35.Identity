using System;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.UseCase.UpdatePhone
{
    public class TransactedUpdatePhoneRequest :
        PipeNodeBase<IUpdatePhoneRequestContract, IUpdatePhoneResultContract>
    {
        private readonly ApplicationContext _context;

        public TransactedUpdatePhoneRequest(
            ApplicationContext context,
            IPipeNode<
                IUpdatePhoneRequestContract,
                IUpdatePhoneResultContract> next) :
            base(next)
        {
            _context = context;
        }

        public override async Task<IUpdatePhoneResultContract> Ask(
            IUpdatePhoneRequestContract input,
            CancellationToken cancellationToken)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var response = await DoNext(input, cancellationToken);
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