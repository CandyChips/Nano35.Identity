using FluentValidation;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Requests.UpdatePhone
{
    public class ValidatorOfUpdateUsersPhoneRequest :
        AbstractValidator<IUpdatePhoneRequestContract>
    {

        public ValidatorOfUpdateUsersPhoneRequest()
        {
            // ToDo
        }
    }
}