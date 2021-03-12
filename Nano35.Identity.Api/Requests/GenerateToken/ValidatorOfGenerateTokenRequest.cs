using FluentValidation;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.GenerateToken
{
    public class ValidatorOfGenerateTokenRequest :
        AbstractValidator<IGenerateTokenRequestContract> 
    {
        public ValidatorOfGenerateTokenRequest()
        {
            RuleFor(token => token.Login)
                .NotEmpty()
                .WithMessage("Не указан номер телефона");
            RuleFor(token => token.Password)
                .NotEmpty()
                .WithMessage("Не указан пароль");
        }
    } 
}