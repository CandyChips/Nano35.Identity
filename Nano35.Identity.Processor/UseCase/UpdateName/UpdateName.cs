using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.UseCase.UpdateName
{
    public class UpdateName : EndPointNodeBase<IUpdateNameRequestContract, IUpdateNameResultContract>
    {
        private readonly ApplicationContext _context;
        public UpdateName(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IUpdateNameResultContract>> Ask(
            IUpdateNameRequestContract request,
            CancellationToken cancellationToken)
        {
            var result = await _context.Users.FirstAsync(a => a.Id == request.UserId.ToString(), cancellationToken);
            result.Name = request.Name;
            return new UseCaseResponse<IUpdateNameResultContract>(new UpdateNameResultContract());
        }
    }
}