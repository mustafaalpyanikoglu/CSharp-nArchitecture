using Application.Features.OperationClaims.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Commands.CreateOperationClaim
{
    public class DeleteOperationClaimCommandValidator:AbstractValidator<DeleteOperationClaimCommand>
    {
        public DeleteOperationClaimCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
