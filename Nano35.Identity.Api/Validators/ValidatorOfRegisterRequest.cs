using FluentValidation;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Validators
{
    public class ValidatorOfRegisterRequest :
        AbstractValidator<IRegisterRequestContract>
    {

        public ValidatorOfRegisterRequest()
        {
            RuleFor(request => request.NewUserId).NotEmpty().WithMessage("userId нет");
            RuleFor(request => request.Phone).NotEmpty().WithMessage("нет номера телефона");
            RuleFor(request => request.Email).NotEmpty().WithMessage("почта проебалась");
            RuleFor(request => request.Password).NotEmpty().WithMessage("пароли для лохов");
            RuleFor(request => request.PasswordConfirm).NotEmpty().Equal(request => request.Password)
                .WithMessage("Пароли не совпадают");
        }
    }
}