using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Processor.UseCase.GetUserById
{
    public class ValidatedGetUserByIdRequestErrorResult :
        IGetUserByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetUserByIdRequest:
        PipeNodeBase<IGetUserByIdRequestContract, IGetUserByIdResultContract>
    {
        public ValidatedGetUserByIdRequest(
            IPipeNode<IGetUserByIdRequestContract, IGetUserByIdResultContract> next) :
            base(next) {}

        public override Task<IGetUserByIdResultContract> Ask(
            IGetUserByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            return DoNext(input, cancellationToken);
        }
    }
}