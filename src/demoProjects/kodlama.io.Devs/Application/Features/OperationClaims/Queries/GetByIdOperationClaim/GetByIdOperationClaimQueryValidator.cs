using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Queries.GetByIdOperationClaim
{
    public class GetByIdOperationClaimQueryValidator:AbstractValidator<GetByIdOperationClaimQuery>
    {
        public GetByIdOperationClaimQueryValidator()
        {
            RuleFor(o => o.Id).NotEmpty();
            RuleFor(o => o.Id).NotNull();
            RuleFor(o => o.Id).GreaterThan(0);
        }
    }
}
