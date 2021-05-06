using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.UseCase
{
    public class TransactedPipeNode<TIn, TOut> : PipeNodeBase<TIn, TOut>
        where TIn : class, IRequest
        where TOut : class, IResult
    {
        private readonly ApplicationContext _context;
        public TransactedPipeNode(ApplicationContext context, IPipeNode<TIn, TOut> next) : base(next) => _context = context;
        public override async Task<UseCaseResponse<TOut>> Ask(TIn input, CancellationToken cancellationToken)
        {
            var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var response = await DoNext(input, cancellationToken);
                if (!response.IsSuccess())
                {
                    await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                    return response;
                }
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return response;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                return Pass($"transaction refused.");
            }
        }
    }
}