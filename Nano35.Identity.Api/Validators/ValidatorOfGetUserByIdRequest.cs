using FluentValidation;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Validators
{
    public class ValidatorOfGetUserByIdRequest :
        AbstractValidator<IGetUserByIdRequestContract> 
    {
        public ValidatorOfGetUserByIdRequest()
        {
            RuleFor(id => id.UserId)
                .NotEmpty()
                .WithMessage("Ошибка сервера. Обновите страницу.");
        }
    } 
}