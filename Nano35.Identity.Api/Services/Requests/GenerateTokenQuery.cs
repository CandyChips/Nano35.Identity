using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Services.Requests
{
    public class GenerateTokenResultViewModel : 
        IGenerateTokenSuccessResultContract,
        IGenerateTokenErrorResultContract
    {
        public string Token { get; set; }
        public string Error { get; set; }
    }

    public class GenerateTokenQuery : 
        IGenerateTokenRequestContract, 
        IRequest<GenerateTokenResultViewModel>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class GenerateTokenQueryValidator : 
        AbstractValidator<GenerateTokenQuery>
    {
        public GenerateTokenQueryValidator()
        {
            RuleFor(x => x.Login)
                .Length(6, 100)
                .NotEmpty();

            RuleFor(x => x.Password)
                .Length(6, 100)
                .NotEmpty();
        }
    }
    
    public class GenerateTokenHandler : 
        IRequestHandler<GenerateTokenQuery, GenerateTokenResultViewModel>
    {
        private readonly ILogger<GenerateTokenHandler> _logger;
        private readonly IBus _bus;
        public GenerateTokenHandler(
            IBus bus, 
            ILogger<GenerateTokenHandler> logger)
        {
            _bus = bus;
            _logger = logger;
        }

        public async Task<GenerateTokenResultViewModel> Handle(
            GenerateTokenQuery message,
            CancellationToken cancellationToken)
        {
            var result = new GenerateTokenResultViewModel();
            var client = _bus.CreateRequestClient<IGenerateTokenRequestContract>(TimeSpan.FromSeconds(10));
            var response = await client
                .GetResponse<IGenerateTokenSuccessResultContract, IGenerateTokenErrorResultContract>(message, cancellationToken);
            
            if (response.Is(out Response<IGenerateTokenSuccessResultContract> successResponse))
            {
                result.Token = successResponse.Message.Token;
                return result;
            }
            
            if (response.Is(out Response<IGenerateTokenErrorResultContract> errorResponse))
            {
                result.Error = errorResponse.Message.Error;
                return result;
            }
            
            throw new InvalidOperationException();
        }
    }
    
}