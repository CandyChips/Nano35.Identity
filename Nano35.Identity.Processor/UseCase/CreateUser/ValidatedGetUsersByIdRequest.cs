using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Processor.UseCase.CreateUser
{
    public class ValidatedCreateUserRequestErrorResult : 
        ICreateUserErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class CreateUserValidator:
        PipeNodeBase<ICreateUserRequestContract, ICreateUserResultContract>
    {
        public CreateUserValidator(
            IPipeNode<ICreateUserRequestContract, ICreateUserResultContract> next) :
            base(next) {}

        public override Task<ICreateUserResultContract> Ask(
            ICreateUserRequestContract input,
            CancellationToken cancellationToken)
        {
            return DoNext(input, cancellationToken);
        }
    }
}