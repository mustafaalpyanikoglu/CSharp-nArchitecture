using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Commands.UpdateOperationClaim
{
    public class UpdateOperationClaimCommandValidator:AbstractValidator<UpdateOperationClaimCommand>
    {
        public UpdateOperationClaimCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Id).GreaterThan(0);

            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Name).MaximumLength(30);
        }
    }
}
