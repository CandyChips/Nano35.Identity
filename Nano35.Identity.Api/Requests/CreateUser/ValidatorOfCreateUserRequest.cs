using FluentValidation;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.CreateUser
{
    public class ValidatorOfCreateUserRequest :
        AbstractValidator<ICreateUserRequestContract>
    {

        public ValidatorOfCreateUserRequest()
        {
            RuleFor(request => request.NewId)
                .NotEmpty()
                .WithMessage("Ошибка сервера. Обновите страницу.");
            RuleFor(request => request.Name)
                .NotEmpty()
                .WithMessage("Введите имя");
            RuleFor(request => request.InstanceId)
                .NotEmpty()
                .WithMessage("Ошибка сервера. Обновите страницу.");
            RuleFor(request => request.CurrentUnitId)
                .NotEmpty()
                .WithMessage("Ошибка сервера. Обновите страницу.");
            RuleFor(request => request.Phone)
                .Matches(@"^\+?[0-9]{1,3}\(?[0-9]{1,3}\)?[0-9]{7}$|[0-9]{3}\-?[0-9]{2}\-?[0-9]{2}$")
                .WithMessage("Некорректный номер телефона");
            RuleFor(request => request.Phone)
                .NotEmpty()
                .WithMessage("Не указан номер телефона");
            RuleFor(request => request.Email)
                .NotEmpty()
                .WithMessage("Не указана электронная почта");
            RuleFor(request => request.Password)
                .NotEmpty()
                .WithMessage("Нет указан пароль");
        }
    }
}