using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Processor.UseCase.GetAllUsers
{
    public class ValidatedGetAllUsersRequest:
        PipeNodeBase<IGetAllUsersRequestContract, IGetAllUsersResultContract>
    {
        public ValidatedGetAllUsersRequest(
            IPipeNode<IGetAllUsersRequestContract, IGetAllUsersResultContract> next) :
            base(next) {}

        public override Task<IGetAllUsersResultContract> Ask(
            IGetAllUsersRequestContract input,
            CancellationToken cancellationToken)
        {
            return DoNext(input, cancellationToken);
        }
    }
}