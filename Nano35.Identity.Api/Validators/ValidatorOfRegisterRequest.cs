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
                .WithMessage("Нет номера телефона");
            RuleFor(request => request.Email).NotEmpty().EmailAddress().WithMessage("Нет почты");
            RuleFor(request => request.Password).NotEmpty().WithMessage("Нет пароля");
            RuleFor(request => request.PasswordConfirm).NotEmpty().Equal(request => request.Password)
                .WithMessage("Пароли не совпадают");
        }
    }
}