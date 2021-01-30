using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Identity.Processor.Models;
using Nano35.Identity.Processor.Requests.Behaviours;
using Nano35.Identity.Processor.Services.Contexts;
using Nano35.Identity.Processor.Services.Helpers;
using Nano35.Identity.Processor.Services.MappingProfiles;

namespace Nano35.Identity.Processor.Requests
{
    public class GetUserByIdQuery :
        IGetUserByIdRequestContract,
        IQueryRequest<IGetUserByIdResultContract>
    {
        public Guid UserId { get; set; }
        
        private class GetUserByIdSuccessResultContract : 
            IGetUserByIdSuccessResultContract
        {
            public IUserViewModel Data { get; set; }
        }
        
        private class GetUserByIdErrorResultContract : 
            IGetUserByIdErrorResultContract
        {
            public string Message { get; set; }
        }
        
        public class GetUserByIdHandler :
            IRequestHandler<GetUserByIdQuery, IGetUserByIdResultContract>
        {
            private readonly ApplicationContext _context;

            public GetUserByIdHandler(ApplicationContext context)
            {
                _context = context;
            }


            public async Task<IGetUserByIdResultContract> Handle(
                GetUserByIdQuery request,
                CancellationToken cancellationToken)
            {
                var result = (await _context.Users.FirstOrDefaultAsync(f => f.Id == request.UserId.ToString(), cancellationToken: cancellationToken)).MapTo<IUserViewModel>();

                if (result == null)
                    return new GetUserByIdErrorResultContract() {Message = "Не найдено"};
                
                return new GetUserByIdSuccessResultContract() {Data = result};
            }
        }
    }
}