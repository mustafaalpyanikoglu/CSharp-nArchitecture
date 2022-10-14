using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Constants
{
    public class AuthBusinessRulesMessages
    {
        public string EmailAlreadyExist => "This email address already exist in the system";
        public string OperationClaimDoesNotExist => "Developer Claim does not exist in the system. Could not add the claim for Developer";
    }
}
