using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Queries.GetByIdUserOperationClaim
{
    public class GetByIdUserOperationClaimQueryValidator:AbstractValidator<GetByIdUserOperationClaimQuery>
    {
        public GetByIdUserOperationClaimQueryValidator()
        {
            RuleFor(u => u.Id).NotEmpty();
            RuleFor(u => u.Id).NotNull();
            RuleFor(u => u.Id).GreaterThan(0);
        }
    }
}
