using FluentValidation;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.UpdatePassword
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