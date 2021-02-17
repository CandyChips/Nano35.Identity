using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Processor.Requests.ConfirmEmailOfUser
{
    public class ConfirmEmailOfUserRequest :
        IPipelineNode<
            IGenerateTokenRequestContract,
            IGenerateTokenResultContract>
    {

        public ConfirmEmailOfUserRequest()
        {
        }

        public async Task<IGenerateTokenResultContract> Ask(
            IGenerateTokenRequestContract input, 
            CancellationToken cancellationToken)
        {
            // ToDo FIX IT NOW!!!
            throw new NotImplementedException();
        }
    }
}
