using FluentValidation;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Validators
{
    public class ValidatorOfGetUserByTokenRequest :
        AbstractValidator<IGetUserByIdRequestContract>
    {
        public ValidatorOfGetUserByTokenRequest()
        {
            RuleFor(token => token.UserId).NotEmpty().WithMessage(
                "Где UserId?");
        }
    }
}