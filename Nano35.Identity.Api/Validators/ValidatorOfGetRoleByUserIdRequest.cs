using FluentValidation;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Validators
{
    public class ValidatorOfGetRoleByUserIdRequest :
        AbstractValidator<IGetRoleByUserIdRequestContract>
    {

        public ValidatorOfGetRoleByUserIdRequest()
        {
            RuleFor(id => id.UserId).NotEmpty().WithMessage("Нет id");
        }
    }
}