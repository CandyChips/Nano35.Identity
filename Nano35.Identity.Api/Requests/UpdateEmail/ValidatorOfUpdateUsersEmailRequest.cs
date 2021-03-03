using FluentValidation;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Validators
{
    public class ValidatorOfUpdateUsersEmailRequest :
        AbstractValidator<IUpdateEmailRequestContract>
    {

        public ValidatorOfUpdateUsersEmailRequest()
        {
            // ToDo
        }
    }
}