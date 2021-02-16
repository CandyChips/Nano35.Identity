using FluentValidation;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Validators
{
    public class ValidatorOfGenerateTokenRequest :
        AbstractValidator<IGenerateTokenRequestContract> 
    {
        public ValidatorOfGenerateTokenRequest()
        {
            RuleFor(token => token.Login).NotEmpty().WithMessage("лОГИН ПУСТОЙ БЛИН");
            RuleFor(token => token.Password).NotEmpty().WithMessage("ПАРОЛЬ ПУСТОЙ БЛЯДЬ");
        }
    } 
}