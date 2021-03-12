using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Api.Helpers;

namespace Nano35.Identity.Api.Requests.Register
{
    public class RegisterRequest :
        EndPointNodeBase<IRegisterRequestContract, IRegisterResultContract>
    {
        private readonly IBus _bus;

        public RegisterRequest(IBus bus)
        {
            _bus = bus;
        }

        public override async Task<IRegisterResultContract> Ask(IRegisterRequestContract input)
        {
            input.Phone = PhoneConverter.RuPhoneConverter(input.Phone);
            
            var client = _bus.CreateRequestClient<IRegisterRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IRegisterSuccessResultContract, IRegisterErrorResultContract>(input);
            
            if (response.Is(out Response<IRegisterSuccessResultContract> successResponse))
            {
                return successResponse.Message;
            }

            if (response.Is(out Response<IRegisterErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}
