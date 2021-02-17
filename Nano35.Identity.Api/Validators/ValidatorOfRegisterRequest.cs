using FluentValidation;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Validators
{
    public class ValidatorOfRegisterRequest :
        AbstractValidator<IRegisterRequestContract>
    {

        public ValidatorOfRegisterRequest()
        {
            //RuleFor(request => request.NewUserId).NotEmpty().WithMessage("userId нет");
            RuleFor(request => request.Phone).NotEmpty()
                .Matches(@"^\+?[0-9]{1,3}\(?[0-9]{1,3}\)?[0-9]{7}$|[0-9]{3}\-?[0-9]{2}\-?[0-9]{2}$")
                .WithMessage("нет номера телефона");
            RuleFor(request => request.Email).NotEmpty().EmailAddress().WithMessage("почта проебалась");
            RuleFor(request => request.Password).NotEmpty().WithMessage("пароли для лохов");
            RuleFor(request => request.PasswordConfirm).NotEmpty().Equal(request => request.Password)
                .WithMessage("Пароли не совпадают");
        }
    }
}