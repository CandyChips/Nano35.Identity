using FluentValidation;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Validators
{
    public class ValidatorOfGetRoleByIdRequest:
        AbstractValidator<IGetRoleByIdRequestContract>
    {

        public ValidatorOfGetRoleByIdRequest()
        {
            RuleFor(id => id.RoleId).NotEmpty().WithMessage("Нет roleId");
        }
    }
}