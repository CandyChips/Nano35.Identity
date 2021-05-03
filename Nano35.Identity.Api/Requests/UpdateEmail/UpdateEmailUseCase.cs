﻿using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Identity.Api.Requests.UpdateEmail
{
    public class UpdateEmailUseCase : UseCaseEndPointNodeBase<IUpdateEmailRequestContract, IUpdateEmailResultContract>
    {
        private readonly IBus _bus;
        public UpdateEmailUseCase(IBus bus) => _bus = bus;

        public override async Task<UseCaseResponse<IUpdateEmailResultContract>> Ask(IUpdateEmailRequestContract input) => 
            await new MasstransitUseCaseRequest<IUpdateEmailRequestContract, IUpdateEmailResultContract>(_bus, input)
                .GetResponse();
    }
}
