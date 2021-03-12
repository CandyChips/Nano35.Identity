using FluentValidation;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.UpdateEmail
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