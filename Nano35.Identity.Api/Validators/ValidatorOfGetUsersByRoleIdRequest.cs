﻿using FluentValidation;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.Identity.Api.Validators
{
    public class ValidatorOfGetUsersByRoleIdRequest :
        AbstractValidator<IGetUsersByRoleIdRequestContract>
    {
        public ValidatorOfGetUsersByRoleIdRequest()
        {
            RuleFor(id => id.RoleId).NotEmpty().WithMessage("нет id");
        }
    }
}