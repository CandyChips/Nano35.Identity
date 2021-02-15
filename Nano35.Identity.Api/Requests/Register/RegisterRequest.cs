﻿using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.Register
{
    public class RegisterRequest :
        IPipelineNode<IRegisterRequestContract, IRegisterResultContract>
    {
        private readonly IBus _bus;

        public RegisterRequest(
            IBus bus)
        {
            _bus = bus;
        }

        public async Task<IRegisterResultContract> Ask(
            IRegisterRequestContract input)
        {
            var client = _bus.CreateRequestClient<IRegisterRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IRegisterSuccessResultContract, IRegisterErrorResultContract>(input);
            
            if (response.Is(out Response<IRegisterSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IRegisterErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}
