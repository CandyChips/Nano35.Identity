﻿using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Identity.Api.Helpers;

namespace Nano35.Identity.Api.Requests.Register
{
    public class Register : EndPointNodeBase<IRegisterRequestContract, IRegisterResultContract>
    {
        private readonly IBus _bus;
        public Register(IBus bus) => _bus = bus;

        public override async Task<UseCaseResponse<IRegisterResultContract>> Ask(IRegisterRequestContract input)
        {
            input.Phone = PhoneConverter.RuPhoneConverter(input.Phone);
            return await new MasstransitRequest<IRegisterRequestContract, IRegisterResultContract>(_bus, input).GetResponse();
        }
    }
}
