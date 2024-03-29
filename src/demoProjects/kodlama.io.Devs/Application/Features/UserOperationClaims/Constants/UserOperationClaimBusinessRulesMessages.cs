﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Constants
{
    public class UserOperationClaimBusinessRulesMessages
    {
        public string UserAlreadyHaveThisOperationClaim => "Given User alreadh have this User Operation Claim!";
        public string UserOperationClaimDoesNotExist => "User Operation Claim does not exist in the system!";
        public string UserDoesNotExist => "User does not exist in the system!";
        public string OperationClaimDoesNotExist => "OperationClaim does not exist in the system!";
        public string UserOperationClaimDataNotExist => "There is no any User Operation Claim data in the system as requested!";
    }
}
