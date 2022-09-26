using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Constants
{
    public class OperationClaimBusinessRulesMessages
    {
        public string OperationClaimNameAlreadyExist => "Operation Claim name already exist in the system!";
        public string OperationClaimDoesNotExist => "Operation Claim does not exist in the system!";
        public string OperationClaimDataNotExist => "There is no any Operation Claim data in the system as requested!";
        public string SomeUsersHaveThisOperationClaim => "Some Users have this Operation Claim. Can not be deleted!";
    }
}
