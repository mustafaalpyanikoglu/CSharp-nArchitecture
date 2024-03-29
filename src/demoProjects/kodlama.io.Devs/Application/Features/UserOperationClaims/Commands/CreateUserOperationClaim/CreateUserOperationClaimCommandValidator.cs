﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Commands.CreateUserOperationClaim
{
    public class CreateUserOperationClaimCommandValidator:AbstractValidator<CreateUserOperationClaimCommand>
    {
        public CreateUserOperationClaimCommandValidator()
        {
            RuleFor(u => u.UserId).NotEmpty();
            RuleFor(u => u.UserId).NotNull();
            RuleFor(u => u.UserId).GreaterThan(0);

            RuleFor(u => u.OperationClaimId).NotEmpty();
            RuleFor(u => u.OperationClaimId).NotNull();
            RuleFor(u => u.OperationClaimId).GreaterThan(0);
        }
    }
}
