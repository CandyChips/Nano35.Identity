using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts;
using Nano35.Contracts.Identity;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Api.Services.Helpers;

namespace Nano35.Identity.Api.Services.Requests
{
    public class UpdatePasswordResultViewModel :
        IUpdatePasswordSuccessResultContract,
        IUpdatePasswordErrorResultContract
    {
        public string Error { get; set; }
    }
    
    public class UpdatePasswordQuery : 
        IUpdatePasswordRequestContract, 
        IRequest<UpdatePasswordResultViewModel>
    {
        public Guid UserId { get; set; }
        public string Password { get; set; }
    }

    public class UpdatePasswordQueryValidator : 
        AbstractValidator<UpdatePasswordQuery>
    {
        public UpdatePasswordQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.Password)
                .Length(6, 100)
                .NotEmpty();
        }
    }

    public class UpdatePasswordHandler : 
        IRequestHandler<UpdatePasswordQuery, UpdatePasswordResultViewModel>
    {
        private readonly IBus _bus;
        private readonly ILogger<UpdatePasswordHandler> _logger;
        private readonly ICustomAuthStateProvider _customAuthStateProvider;
        public UpdatePasswordHandler(
            IBus bus, 
            ILogger<UpdatePasswordHandler> logger, 
            ICustomAuthStateProvider customAuthStateProvider)
        {
            _bus = bus;
            _customAuthStateProvider = customAuthStateProvider;
            _logger = logger;
        }

        public async Task<UpdatePasswordResultViewModel> Handle(
            UpdatePasswordQuery message,
            CancellationToken cancellationToken)
        {
            var result = new UpdatePasswordResultViewModel();
            var client = _bus.CreateRequestClient<IUpdatePasswordRequestContract>(TimeSpan.FromSeconds(10));
            var response = await client
                .GetResponse<IUpdatePasswordSuccessResultContract, IUpdatePasswordErrorResultContract>(message, cancellationToken);

            if (response.Is(out Response<IUpdatePasswordSuccessResultContract> successResponse))
            {
                return result;
            }
            
            if (response.Is(out Response<IUpdatePasswordErrorResultContract> errorResponse))
            {
                result.Error = "Не найдено";
                return result;
            }
            
            throw new InvalidOperationException();
        }
    }
}