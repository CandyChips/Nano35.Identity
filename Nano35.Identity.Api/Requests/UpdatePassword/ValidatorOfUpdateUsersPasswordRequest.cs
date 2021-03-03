using FluentValidation;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Validators
{
    public class ValidatorOfUpdateUsersPasswordRequest :
        AbstractValidator<IUpdatePasswordRequestContract>
    {

        public ValidatorOfUpdateUsersPasswordRequest()
        {
            // ToDo
        }
    }
}