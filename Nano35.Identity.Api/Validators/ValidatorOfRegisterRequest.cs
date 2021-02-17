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
                .Matches("/^[+]{1}[0-9]{1}[(]{1}[0-9]{3}[)]{1}[0-9]{3}[-]{1}[0-9]{2}[-]{1}[0-9]{2}$/").WithMessage("нет номера телефона");
            RuleFor(request => request.Email).NotEmpty().WithMessage("почта проебалась");
            RuleFor(request => request.Password).NotEmpty().WithMessage("пароли для лохов");
            RuleFor(request => request.PasswordConfirm).NotEmpty().Equal(request => request.Password)
                .WithMessage("Пароли не совпадают");
        }
    }
}