using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Identity.Api.Services.Helpers;

namespace Nano35.Identity.Api.Requests
{
    public class UpdatePhoneQuery :
        IUpdatePhoneRequestContract,
        IRequest<IUpdatePhoneResultContract>
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
        IRequestHandler<UpdatePhoneQuery, IUpdatePhoneResultContract>
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

        public async Task<IUpdatePhoneResultContract> Handle(
            UpdatePhoneQuery message,
            CancellationToken cancellationToken)
        {
            var client = _bus.CreateRequestClient<IUpdatePhoneRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IUpdatePhoneSuccessResultContract, IUpdatePhoneErrorResultContract>(message, cancellationToken);
            
            if (response.Is(out Response<IUpdatePhoneSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IUpdatePhoneErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}