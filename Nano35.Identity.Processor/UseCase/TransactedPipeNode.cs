using System;
using System.Threading;
using System.Threading.Tasks;
using LanguageExt;
using Nano35.Contracts;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.UseCase
{
    public class TransactedRailPipeNode<TIn, TOut> :
        RailPipeNodeBase<TIn, TOut>
        where TIn : IRequest
        where TOut : ISuccess
    {
        private readonly ApplicationContext _context;

        public TransactedRailPipeNode(
            ApplicationContext context,
            IRailPipeNode<TIn, TOut> next) : base(next)
        {
            _context = context;
        }

        public override async Task<Either<string, TOut>> Ask(TIn input, CancellationToken cancellationToken)
        {
            var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var response = await DoNext(input, cancellationToken);
                response.Match(
                    _ => {},
                    async _ => 
                        {await _context.SaveChangesAsync(cancellationToken);
                         await transaction.CommitAsync(cancellationToken);});
                return response;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                return "Транзакция отменена";
            }
        }
    }
}