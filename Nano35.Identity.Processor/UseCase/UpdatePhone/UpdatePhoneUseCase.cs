using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Identity.Processor.Models;
using Nano35.Identity.Processor.Services.Contexts;

namespace Nano35.Identity.Processor.UseCase.UpdatePhone
{
    public class UpdatePhoneUseCase : UseCaseEndPointNodeBase<IUpdatePhoneRequestContract, IUpdatePhoneResultContract>
    {
        private readonly ApplicationContext _context;
        public UpdatePhoneUseCase(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IUpdatePhoneResultContract>> Ask(
            IUpdatePhoneRequestContract request,
            CancellationToken cancellationToken)
        {
            var result = await _context.Users.FirstAsync(a => a.Id == request.UserId.ToString(), cancellationToken);
            result.PhoneNumber = request.Phone;
            return new UseCaseResponse<IUpdatePhoneResultContract>(new UpdatePhoneResultContract());
        }
    }
}