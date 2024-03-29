﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Commands.UpdateUserOperationClaim
{
    public class UpdateUserOperationClaimCommandValidator:AbstractValidator<UpdateUserOperationClaimCommand>
    {
        public UpdateUserOperationClaimCommandValidator()
        {
            RuleFor(u => u.Id).NotEmpty();
            RuleFor(u => u.Id).NotNull();
            RuleFor(u => u.Id).GreaterThan(0);

            RuleFor(u => u.UserId).NotEmpty();
            RuleFor(u => u.UserId).NotNull();
            RuleFor(u => u.UserId).GreaterThan(0);

            RuleFor(u => u.OperationClaimId).NotEmpty();
            RuleFor(u => u.OperationClaimId).NotNull();
            RuleFor(u => u.OperationClaimId).GreaterThan(0);
        }
    }
}
