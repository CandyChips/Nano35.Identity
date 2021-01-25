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
    public class UpdatePhoneResultViewModel :
        IUpdatePhoneSuccessResultContract,
        IUpdatePhoneErrorResultContract
    {
        public string Error { get; set; }
    }

    public class UpdatePhoneQuery :
        IUpdatePhoneRequestContract,
        IRequest<UpdatePhoneResultViewModel>
    {
        public Guid UserId { get; set; }
        public string Phone { get; set; }
    }

    public class UpdatePhoneQueryValidator : 
        AbstractValidator<UpdatePhoneQuery>
    {
        public UpdatePhoneQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.Phone)
                .Length(10)
                .NotEmpty();
        }
    }


    public class UpdatePhoneHandler :
        IRequestHandler<UpdatePhoneQuery, UpdatePhoneResultViewModel>
    {
        private readonly IBus _bus;
        private readonly ILogger<UpdatePhoneHandler> _logger;
        private readonly ICustomAuthStateProvider _customAuthStateProvider;
        public UpdatePhoneHandler(
            IBus bus, 
            ICustomAuthStateProvider customAuthStateProvider,
            ILogger<UpdatePhoneHandler> logger)
        {
            _bus = bus;
            _customAuthStateProvider = customAuthStateProvider;
            _logger = logger;
        }

        public async Task<UpdatePhoneResultViewModel> Handle(
            UpdatePhoneQuery message,
            CancellationToken cancellationToken)
        {
            var result = new UpdatePhoneResultViewModel();
            var client = _bus.CreateRequestClient<IUpdatePhoneRequestContract>(TimeSpan.FromSeconds(10));
            var response = await client
                .GetResponse<IUpdatePhoneSuccessResultContract, IUpdatePhoneErrorResultContract>(message, cancellationToken);

            if (response.Is(out Response<IUpdatePhoneSuccessResultContract> successResponse))
            {
                return result;
            }
            
            if (response.Is(out Response<IUpdatePhoneErrorResultContract> errorResponse))
            {
                result.Error = "Не найдено";
                return result;
            }
            
            throw new InvalidOperationException();
        }
    }
}