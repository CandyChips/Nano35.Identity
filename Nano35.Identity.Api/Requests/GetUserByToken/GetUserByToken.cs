﻿using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Identity.Api.Helpers;

namespace Nano35.Identity.Api.Requests.GetUserByToken
{
    public class GetUserByToken : EndPointNodeBase<IGetUserByIdRequestContract, IGetUserByIdResultContract>
    {
        private readonly IBus _bus;
        private readonly ICustomAuthStateProvider _auth;
        
        public GetUserByToken(IBus bus, ICustomAuthStateProvider auth)
        {
            _bus = bus;
            _auth = auth;
        }
        
        public override async Task<UseCaseResponse<IGetUserByIdResultContract>> Ask(IGetUserByIdRequestContract input)
        {
            input.UserId = _auth.CurrentUserId;
            return await new MasstransitRequest<IGetUserByIdRequestContract, IGetUserByIdResultContract>(_bus, input).GetResponse();
        }
    }
}
