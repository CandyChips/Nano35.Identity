using FluentValidation;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Validators
{
    public class ValidatorOfCreateUserRequest :
        AbstractValidator<ICreateUserRequestContract>
    {

        public ValidatorOfCreateUserRequest()
        {
            RuleFor(request => request.NewId).NotEmpty().WithMessage("Id нет");
            RuleFor(request => request.Name).NotEmpty().WithMessage("Введите имя");
            RuleFor(request => request.InstanceId).NotEmpty().WithMessage("InstanceId нет");
            RuleFor(request => request.CurrentUnitId).NotEmpty().WithMessage("CurrentUnitId нет");
            RuleFor(request => request.Phone).NotEmpty()
                .Matches(@"^\+?[0-9]{1,3}\(?[0-9]{1,3}\)?[0-9]{7}$|[0-9]{3}\-?[0-9]{2}\-?[0-9]{2}$")
                .WithMessage("Нет или не правильный вид номера телефона");
            RuleFor(request => request.Email).NotEmpty().EmailAddress().WithMessage("Нет почты");
            RuleFor(request => request.Password).NotEmpty().WithMessage("Нет пароля");
        }
    }
}