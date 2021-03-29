using System;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.UseCase.CreateUser
{
    public class TransactedCreateUserRequest :
        PipeNodeBase<
            ICreateUserRequestContract,
            ICreateUserResultContract>
    {
        private readonly ApplicationContext _context;

        public TransactedCreateUserRequest(
            ApplicationContext context,
            IPipeNode<
                ICreateUserRequestContract,
                ICreateUserResultContract> next) :
            base(next)
        {
            _context = context;
        }

        public override async Task<ICreateUserResultContract> Ask(
            ICreateUserRequestContract input,
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