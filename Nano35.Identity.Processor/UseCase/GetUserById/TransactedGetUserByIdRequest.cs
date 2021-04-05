using System;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.UseCase.GetUserById
{
    public class TransactedGetUserByIdRequest :
        PipeNodeBase<IGetUserByIdRequestContract, IGetUserByIdResultContract>
    {
        private readonly ApplicationContext _context;

        public TransactedGetUserByIdRequest(
            ApplicationContext context,
            IPipeNode<
                IGetUserByIdRequestContract,
                IGetUserByIdResultContract> next) :
            base(next)
        {
            _context = context;
        }

        public override async Task<IGetUserByIdResultContract> Ask(
            IGetUserByIdRequestContract input,
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