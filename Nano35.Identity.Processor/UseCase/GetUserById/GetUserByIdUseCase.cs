﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Identity.Processor.Services.Contexts;
using Nano35.Identity.Processor.Services.MappingProfiles;

namespace Nano35.Identity.Processor.UseCase.GetUserById
{
    public class GetUserByIdUseCase :
        EndPointNodeBase<IGetUserByIdRequestContract, IGetUserByIdResultContract>
    {
        private readonly ApplicationContext _context;

        public GetUserByIdUseCase(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetUserByIdSuccessResultContract : 
            IGetUserByIdSuccessResultContract
        {
            public IUserViewModel Data { get; set; }
        }

        private class GetAllClientStatesErrorResultContract : 
            IGetUserByIdErrorResultContract
        {
            public string Message { get; set; }
        }

        public override async Task<IGetUserByIdResultContract> Ask(
            IGetUserByIdRequestContract request,
            CancellationToken cancellationToken)
        {
            var result = (await _context.Users.FirstOrDefaultAsync(f => f.Id == request.UserId.ToString(), cancellationToken: cancellationToken)).MapTo<IUserViewModel>();

            if (result == null)
                return new GetAllClientStatesErrorResultContract() {Message = "Не найдено"};
                
            return new GetUserByIdSuccessResultContract() {Data = result};
        }
    }
}