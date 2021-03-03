using FluentValidation;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Validators
{
    public class ValidatorOfRegisterRequest :
        AbstractValidator<IRegisterRequestContract>
    {

        public ValidatorOfRegisterRequest()
        {
            RuleFor(request => request.Phone)
                .Matches(@"^\+?[0-9]{1,3}\(?[0-9]{1,3}\)?[0-9]{7}$|[0-9]{3}\-?[0-9]{2}\-?[0-9]{2}$")
                .WithMessage("Некорректный номер телефона");
            RuleFor(request => request.Phone)
                .NotEmpty()
                .WithMessage("Не указан номер телефона");
            RuleFor(request => request.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Не указана почта");
            RuleFor(request => request.Password)
                .NotEmpty()
                .WithMessage("Не указан пароль");
            RuleFor(request => request.PasswordConfirm)
                .NotEmpty()
                .Equal(request => request.Password)
                .WithMessage("Пароли не совпадают");
        }
    }
}