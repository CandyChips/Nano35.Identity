using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests
{
    public class RegisterCommand :
        IRegisterRequestContract, 
        IRequest<IRegisterResultContract>
    {
        public Guid NewUserId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }

    public class RegisterCommandValidator : 
        AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Email)
                .Length(6, 100)
                .NotEmpty();

            RuleFor(x => x.Password)
                .Length(6, 100)
                .NotEmpty();

            RuleFor(x => x.Phone)
                .Length(9, 100)
                .NotEmpty();
        }
    }
    
    public class RegisterHandler : 
        IRequestHandler<RegisterCommand, IRegisterResultContract>
    {
        private readonly ILogger<RegisterHandler> _logger;
        private readonly IBus _bus;
        public RegisterHandler(
            IBus bus, 
            ILogger<RegisterHandler> logger)
        {
            _bus = bus;
            _logger = logger;
        }
        
        public async Task<IRegisterResultContract> Handle(
            RegisterCommand message, 
            CancellationToken cancellationToken)
        {
            var client = _bus.CreateRequestClient<IRegisterRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IRegisterSuccessResultContract, IRegisterErrorResultContract>(message, cancellationToken);
            
            if (response.Is(out Response<IRegisterSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IRegisterErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}